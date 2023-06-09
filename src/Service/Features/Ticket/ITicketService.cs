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
using ParkingSpace.Helpers;

namespace ParkingSpace.Features.Ticket;

public interface ITicketService : IGenericService<Entities.Ticket> {
    Task<Response<double>> GetPriceAsync(Entities.Ticket entity);
    Task<Response<Entities.Ticket?>> GetActiveByVehicleAsync(Vehicle.Entities.Vehicle vehicle);
    Task<Response<Entities.Ticket?>> ParkVehicleAsync(SpotVehicleParams option);
    Task<Response<Entities.Ticket>> UnParkVehicleAsync(Entities.Ticket entity);
}