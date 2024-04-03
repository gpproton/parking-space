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

namespace ParkingSpace.Features.Vehicle;

public class VehicleService : GenericService<Entities.Vehicle>, IVehicleService {
    private readonly IRepository<Entities.Vehicle> _repository;

    public VehicleService(IRepository<Entities.Vehicle> repository) : base(repository) {
        _repository = repository;
    }
    public async Task<Response<Entities.Vehicle?>> GetByRegistrationNoAsync(string registrationNo) {
        var value = await _repository.GetQueryable()
        .Where(x => x.RegistrationNo.ToLower().Contains(registrationNo.ToLower()))
        .FirstOrDefaultAsync();

        return new Response<Entities.Vehicle?>(value);
    }
}