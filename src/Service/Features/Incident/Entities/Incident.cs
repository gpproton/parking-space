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

namespace ParkingSpace.Features.Incident.Entities;

public class Incident : AuditableEntity<Guid> {
    [Required]
    public string Subject { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset? OccurredAt { get; set; }
    public Space.Entities.Space? Space { get; set; }
    public Guid? SpaceId { get; set; }
    public Vehicle.Entities.Vehicle? Vehicle { get; set; }
    public Guid? VehicleId { get; set; }
}