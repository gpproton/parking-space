// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ParkingSpace.Data;

namespace ParkingSpace.Tests;

public class ServiceFactory : WebApplicationFactory<Program> {
    protected override IHost CreateHost(IHostBuilder builder) {
        Program.UseProxy = false;
        builder.ConfigureServices(services => {
            services.RemoveAll(typeof(DbContextOptions<MainContext>));
            services.RemoveAll(typeof(DbConnection));

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container => {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<MainContext>((container, options) => {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            }
            );
        });

        return base.CreateHost(builder);
    }
}