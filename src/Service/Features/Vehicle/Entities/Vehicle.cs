// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.ComponentModel.DataAnnotations;
using ParkingSpace.Common.Entity;
using ParkingSpace.Enums;

namespace ParkingSpace.Features.Vehicle.Entities;

public class Vehicle : AuditableEntity<Guid> {
    [Required]
    public string RegistrationNo { get; set; } = null!;
    [Required]
    public VehicleType Type { get; set; }
    public string? Color { get; set; }
    public virtual Customer.Entities.Customer? Customer { get; set; }
    public Guid? CustomerId { get; set; }
}