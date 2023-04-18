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
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Features.Price.Entities;

public class Price : AuditableEntity<Guid> {
    [Required]
    public string? Description { get; set; }
    public Spot? Spot { get; set; }
    public Guid? SpotId { get; set; }
    public TimeSpan MaximumTime { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
}