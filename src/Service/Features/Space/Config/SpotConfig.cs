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
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Features.Space.Config;

public class SpotConfig : IEntityTypeConfiguration<Spot> {
    public void Configure(EntityTypeBuilder<Spot> builder) {
        builder
        .Property(e => e.VehicleType)
        .HasConversion(new EnumCollectionJsonValueConverter<VehicleType>())
        .Metadata.SetValueComparer(new CollectionValueComparer<VehicleType>());
    }
}