// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.EntityFrameworkCore;
using ParkingSpace.Common.Entity;
using ParkingSpace.Common.Response;
using ParkingSpace.Features.Space;
using ParkingSpace.Helpers;

namespace ParkingSpace.Features.Ticket;

public class TicketService : GenericService<Entities.Ticket>, ITicketService {
    private readonly IReadRepository<Entities.Ticket> _read;
    private readonly IRepository<Entities.Ticket> _repository;
    private readonly IReadRepository<Price.Entities.Price> _price;
    private readonly ISpotService _spotService;
    public TicketService(
        IRepository<Entities.Ticket> repository,
        IReadRepository<Entities.Ticket> read,
        IReadRepository<Price.Entities.Price> price,
        ISpotService spotService) : base(repository) {
        _repository = repository;
        _read = read;
        _price = price;
        _spotService = spotService;
    }

    public async Task<Response<double>> GetPriceAsync(Entities.Ticket entity) {
        var ticket = await _read.GetQueryable()
        .Where(x => x.Id.Equals(entity.Id))
        .Include(x => x.Vehicle)
        .Include(x => x.Spot)
        .FirstOrDefaultAsync();

        var spot = ticket!.Spot;

        if (spot is null)
            return new Response<double>(0, String.Empty, false);

        var prices = await _price.GetQueryable()
        .Where(x =>
            x.SpaceId == spot!.SpaceId
            && x.VehicleType.Equals(spot.VehicleType))
        .ToListAsync();

        var charge = PriceHelper.CalculatePrice(ticket, prices);

        return new Response<double>(charge, String.Empty, false);
    }

    public async Task<Response<Entities.Ticket?>> ParkVehicleAsync(SpotVehicleParams option) {
        // Check spot availability
        var spotAvailable = await _spotService.CheckAvailabilityAsync(option);
        if (spotAvailable.Data > 0)
            return new Response<Entities.Ticket?>(null, spotAvailable.Message);

        // Create Ticket
        var spot = await _spotService.GetByVehicleAsync(option);
        var ticket = new Entities.Ticket {
            TicketNumber = Guid.NewGuid().ToString()[..6],
            SpotPosition = spotAvailable.Data++,
            SpotId = spot.Data!.Id,
            VehicleId = option.Vehicle.Id,
            StartedAt = DateTimeOffset.Now,
        };
        var result = await _repository.AddAsync(ticket);

        return new Response<Entities.Ticket?>(result, "Success");
    }

    public async Task<Response<Entities.Ticket>> UnParkVehicleAsync(Entities.Ticket entity) {
        var price = await this.GetPriceAsync(entity);
        entity.Amount = price.Data;
        entity.CompletedAt = DateTimeOffset.Now;
        entity.Paid = true;

        await _repository.UpdateAsync(entity);

        var spot = entity.Spot;
        spot!.AvailableSpot -= 1;
        await _spotService.UpdateAsync(spot);

        return new Response<Entities.Ticket>(entity);
    }
}