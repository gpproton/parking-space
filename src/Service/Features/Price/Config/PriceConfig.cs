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
using ParkingSpace.Common.Converter;
using ParkingSpace.Enums;

namespace ParkingSpace.Features.Price.Config;

public class PriceConfig : IEntityTypeConfiguration<Entities.Price> {
    public void Configure(EntityTypeBuilder<Entities.Price> builder) {
        builder
        .Property(e => e.VehicleType)
        .HasConversion(new EnumCollectionJsonValueConverter<VehicleType>())
        .Metadata.SetValueComparer(new CollectionValueComparer<VehicleType>());

        builder
        .HasAlternateKey(x => new {
            x.SpaceId,
            x.VehicleType,
            x.ChargeModel,
            x.MaximumTime
        });

        builder
        .HasOne<Space.Entities.Space>(x => x.Space)
        .WithMany(m => m.Prices)
        .HasForeignKey(f => f.SpaceId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}