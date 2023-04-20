// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.DependencyInjection;
using ParkingSpace.Features.Space;
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Tests.Services;

[Collection("api-context")]
public class SpaceServiceTests {
    private readonly AsyncServiceScope _scope;
    
    public SpaceServiceTests(ServiceFactory factory) {
        _scope = factory.Services.CreateAsyncScope();
    }

    [Fact]
    public async Task TestSpaceServiceGetAll() {
        var service = _scope.ServiceProvider.GetService<ISpaceService>();
        var check = await service!.GetAllAsync(null);
        
        Assert.NotNull(check.Data);
    }
    
    [Fact]
    public async Task TestSpaceServiceCreate() {
        var service = _scope.ServiceProvider.GetService<ISpaceService>();
        var check = await service!.AddAsync(new Space {
            Description = "Test-00"
        });
        
        Assert.NotNull(check.Data);
    }
}