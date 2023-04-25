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
using ParkingSpace.Data.Seeds;
using ParkingSpace.Enums;
using ParkingSpace.Features.Price.Entities;
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Data;

public class DbInitializer : IDbInitializer {
    private readonly IServiceScopeFactory _scopeFactory;

    public DbInitializer(IServiceScopeFactory scopeFactory) {
        this._scopeFactory = scopeFactory;
    }

    public async Task Initialize(CancellationToken cancellationToken = default!) {
        using var serviceScope = _scopeFactory.CreateScope();
        await using var context = serviceScope.ServiceProvider.GetService<MainContext>();
        if (context is null) return;
        if (context.Database.IsRelational())
            await context.Database.MigrateAsync(cancellationToken);
    }

    public async Task SeedData(CancellationToken cancellationToken = default!) {
        using var serviceScope = _scopeFactory.CreateScope();
        await using var context = serviceScope.ServiceProvider.GetService<MainContext>();
        if (context is null) return;

        if (!context.Spaces.Any()) {
            var spaces = SpaceSeeds.GetSpaces();
            await context.Spaces.AddRangeAsync(spaces, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

    }


}