// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ParkingSpace.Common.Response;
using ParkingSpace.Enums;
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Features.Space;

public interface ISpotService : IGenericService<Spot> {
    Task<Response<Entities.Spot?>> GetByVehicleTypeAsync(Entities.Space space, Vehicle.Entities.Vehicle vehicle);
    Task<Response<Entities.Spot?>> GetByVehicleAsync(Entities.Space space, Vehicle.Entities.Vehicle vehicle);
    Task<Response<Entities.Spot?>> GetByTagAsync(string tag);
    Task<Response<int>> CheckSpotAvailableAsync(Entities.Space space, Vehicle.Entities.Vehicle vehicle);
}