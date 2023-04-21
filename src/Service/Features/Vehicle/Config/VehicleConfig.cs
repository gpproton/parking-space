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

namespace ParkingSpace.Features.Vehicle.Config;

public class VehicleConfig : IEntityTypeConfiguration<Entities.Vehicle> {

    public void Configure(EntityTypeBuilder<Entities.Vehicle> builder) {
        builder.Property(x => x.RegistrationNo).IsRequired();
        builder.HasIndex(x => x.RegistrationNo).IsUnique();

        builder
        .HasOne<Customer.Entities.Customer>(x => x.Customer)
        .WithMany()
        .HasForeignKey(f => f.CustomerId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}