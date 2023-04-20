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

namespace ParkingSpace.Features.Ticket;

public class TicketService : GenericService<Entities.Ticket>, ITicketService {
    private readonly IReadRepository<Entities.Ticket> _read;
    private readonly IReadRepository<Price.Entities.Price> _price;
    public TicketService(IRepository<Entities.Ticket> repository, IReadRepository<Entities.Ticket> read, IReadRepository<Price.Entities.Price> price) : base(repository) {
        _read = read;
        _price = price;
    }

    public async Task<Response<double?>> GetPriceAsync(Entities.Ticket entity) {
        var ticket = await _read.GetQueryable()
        .Where(x => x.Id.Equals(entity.Id))
        .Include(x => x.Vehicle)
        .Include(x => x.Spot)
        .FirstOrDefaultAsync();

        var spot = ticket!.Spot;

        if (spot == null)
            return new Response<double?>(null, String.Empty, false);
        
        var timeSpent = ((DateTimeOffset.Now - ticket.StartedAt)!).Value.TotalHours;
        var prices = await _price.GetQueryable()
        .Where(x => 
            x.SpaceId == spot!.SpaceId
            && x.MaximumTime <= timeSpent
            && x.VehicleType.Equals(spot.VehicleType))
        .ToListAsync();
        
        // TODO: Iterate calculate fixed flat rate
        // TODO: Get standard rate
        // TODO: Get per hours rate
        
        return new Response<double?>(timeSpent, String.Empty, false);
    }
    
    public async Task<Response<Entities.Ticket>> ParkVehicleAsync<TId>(Entities.Ticket entity) {
        throw new NotImplementedException();
    }
    
    public async Task<Response<Entities.Ticket>> UnParkVehicleAsync<TId>(Entities.Ticket entity) {
        throw new NotImplementedException();
    }
}