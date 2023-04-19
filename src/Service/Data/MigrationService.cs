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

namespace ParkingSpace.Data;

public class MigrationService : BackgroundService {
    private readonly ILogger<MigrationService> _logger;
    private readonly IDbInitializer _initializer;
    
    public MigrationService(ILogger<MigrationService> logger, IServiceScopeFactory factory) {
        _logger = logger;
        _initializer = factory.CreateScope().ServiceProvider.GetRequiredService<IDbInitializer>();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _logger.LogInformation("Starting migration & seeding...");
        await _initializer.Initialize(stoppingToken);
        await _initializer.SeedData(stoppingToken);
        _logger.LogInformation("Completed migration & seed process...");
    }
}