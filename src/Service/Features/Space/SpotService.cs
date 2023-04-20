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
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Features.Space;

public class SpotService : GenericService<Spot>, ISpotService {
    private readonly IReadRepository<Spot> _read;
    private readonly IReadRepository<Ticket.Entities.Ticket> _ticket;
    public SpotService(IRepository<Spot> repository, IReadRepository<Spot> readRepository, IReadRepository<Ticket.Entities.Ticket> ticket) : base(repository) {
        _read = readRepository;
        _ticket = ticket;
    }

    public async Task<Response<Spot?>> GetByVehicleTypeAsync(Entities.Space space, Vehicle.Entities.Vehicle vehicle) {
        var value = await _read.GetQueryable()
            .FirstOrDefaultAsync(x => 
            x.SpaceId == space.Id
            && x.Active
            && x.VehicleType.Contains(vehicle.Type));
        
        return new Response<Spot?>(value, string.Empty, value != null);
    }
    public async Task<Response<Spot?>> GetByVehicleAsync(Entities.Space space, Vehicle.Entities.Vehicle vehicle) {
        var value = await _read.GetQueryable()
            .FirstOrDefaultAsync(x =>
            x.SpaceId.Equals(space.Id) 
            && x.VehicleType.Contains(vehicle.Type));

        return new Response<Spot?>(value, string.Empty, value != null);
    }
    public async Task<Response<Spot?>> GetByTagAsync(string tag) {
        var value = await _read.GetQueryable()
            .FirstOrDefaultAsync(x =>
            x.Active
            && x.Tag.Equals(tag));
        
        return new Response<Spot?>(value, string.Empty, value != null);
    }
    public async Task<Response<int>> CheckSpotAvailableAsync(Entities.Space space, Vehicle.Entities.Vehicle vehicle) {
        var spot = await _read.GetQueryable()
                    .FirstOrDefaultAsync(x => 
                    x.SpaceId == space.Id
                    && x.Active
                    && x.VehicleType.Contains(vehicle.Type));

        if (spot == null)
            return new Response<int>(0, String.Empty, false);

        var activeSpots = await _ticket.GetQueryable()
                    .CountAsync(x =>
                    x.SpotId.Equals(spot.Id)
                    && x.StartedAt != null
                    && x.CompletedAt == null);
        
        return new Response<int>(activeSpots, String.Empty);
    }
}