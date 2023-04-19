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
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkingSpace.Features.Incident.Config;

public class IncidentConfig : IEntityTypeConfiguration<Entities.Incident> {
    public void Configure(EntityTypeBuilder<Entities.Incident> builder) {
        builder
        .HasOne<Space.Entities.Space>(x => x.Space)
        .WithMany()
        .HasForeignKey(f => f.SpaceId)
        .OnDelete(DeleteBehavior.SetNull);
        
        builder
        .HasOne<Vehicle.Entities.Vehicle>(x => x.Vehicle)
        .WithMany()
        .HasForeignKey(f => f.VehicleId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}