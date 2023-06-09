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
using ParkingSpace.Common.Interfaces;
using ParkingSpace.Common.Response;
using ParkingSpace.Features.Space;
using ParkingSpace.Filters;
using ParkingSpace.Helpers;

namespace ParkingSpace.Features.Ticket {
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

        public new async Task<PagedResponse<IEnumerable<Entities.Ticket>>> GetAllAsync(IPageFilter? filter) {
            IPageFilter checkFilter = filter ?? new PageFilter();
            int count = await _repository.GetQueryable().CountAsync();
            List<Entities.Ticket> result = await _repository
                         .GetQueryable(checkFilter)
                         .Where(x => x.CompletedAt == null)
                         .ToListAsync();

            return new PagedResponse<IEnumerable<Entities.Ticket>>(result, checkFilter.Page, checkFilter.PageSize, count);
        }

        public async Task<Response<double>> GetPriceAsync(Entities.Ticket entity) {
            Entities.Ticket? ticket = await _read.GetQueryable()
            .Where(x => x.Id.Equals(entity.Id))
            .Include(x => x.Vehicle)
            .Include(x => x.Spot)
            .FirstOrDefaultAsync();

            Space.Entities.Spot? spot = ticket!.Spot;
            if (spot is null) {
                return new Response<double>(0, String.Empty, false);
            }

            List<Price.Entities.Price> prices = await _price.GetQueryable().Where(x => x.SpaceId == spot.SpaceId).ToListAsync();
            List<Price.Entities.Price> requiredPrices = prices.Where(x => x.VehicleType.Contains(entity.Vehicle!.Type)).ToList();
            entity.CompletedAt ??= DateTimeOffset.Now;
            double charge = PriceResolver.CalculatePrice(ticket, requiredPrices);

            return new Response<double>(charge, String.Empty, false);
        }
        public async Task<Response<Entities.Ticket?>> GetActiveByVehicleAsync(Vehicle.Entities.Vehicle vehicle) {
            Entities.Ticket? result = await _repository.GetQueryable()
                        .Where(x => x.VehicleId.Equals(vehicle.Id)
                            && x.CompletedAt == null
                            )
                        .FirstOrDefaultAsync();

            return new Response<Entities.Ticket?>(result);
        }

        public async Task<Response<Entities.Ticket?>> ParkVehicleAsync(SpotVehicleParams option) {
            // Check spot availability
            Response<int> totalSpotAvailable = await _spotService.CheckAvailabilityAsync(option);
            if (totalSpotAvailable.Data < 1) {
                return new Response<Entities.Ticket?>(null, totalSpotAvailable.Message, false);
            }

            // Create Ticket
            Space.Entities.Spot? spot = (await _spotService.GetByVehicleAsync(option)).Data;
            int vehicleSpotNumber = spot!.MaximumSpot - totalSpotAvailable.Data + 1;
            spot.AvailableSpot -= 1;
            Entities.Ticket ticket = new Entities.Ticket {
                TicketNumber = Guid.NewGuid().ToString()[..8].ToUpper(),
                SpotPosition = vehicleSpotNumber,
                SpotId = spot!.Id,
                VehicleId = option.Vehicle.Id,
                StartedAt = option.Time,
            };

            Entities.Ticket result = await _repository.AddAsync(ticket);
            _ = await _spotService.UpdateAsync(spot);

            return new Response<Entities.Ticket?>(result, "Success");
        }

        public async Task<Response<Entities.Ticket>> UnParkVehicleAsync(Entities.Ticket entity) {
            entity.CompletedAt ??= DateTimeOffset.Now;
            Response<double> price = await GetPriceAsync(entity);
            entity.Amount = price.Data;
            entity.Paid = true;
            await _repository.UpdateAsync(entity);

            Space.Entities.Spot? spot = entity.Spot;
            spot!.AvailableSpot += 1;
            _ = await _spotService.UpdateAsync(spot);

            return new Response<Entities.Ticket>(entity);
        }
    }
}