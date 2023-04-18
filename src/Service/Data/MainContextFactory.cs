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
using Microsoft.EntityFrameworkCore.Design;

namespace ParkingSpace.Data;

public class MainContextFactory : IDesignTimeDbContextFactory<MainContext> {
    public MainContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
        var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlite(connectionString, b => b
        .MigrationsAssembly(typeof(MainContext).Assembly.FullName)
        .UseRelationalNulls());

        return new MainContext(optionsBuilder.Options);
    }
}