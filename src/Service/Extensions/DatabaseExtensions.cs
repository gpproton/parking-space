// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using ParkingSpace.Common.Entity;
using ParkingSpace.Data;

namespace ParkingSpace.Extensions;

public static class DatabaseExtensions {
    public static IServiceCollection RegisterDataContext(this IServiceCollection services) {
        var sp = services.BuildServiceProvider();
        var config = sp.GetService<IConfiguration>();
        services.AddDbContext<MainContext>(options =>
        options.UseSqlite(config!.GetConnectionString("DefaultConnection"),
            b => b
            .MigrationsAssembly(typeof(MainContext).Assembly.FullName)
            .UseRelationalNulls()
            )
        );
        
        services.Configure<JsonOptions>(options => {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(GenericRepository<>));
        services.AddHostedService<MigrationService>();

        return services;
    }
}