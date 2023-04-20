// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Net.Http.Json;
using System.Text.Json;
using ParkingSpace.Common;
using ParkingSpace.Common.Response;
using ParkingSpace.Features.Space.Entities;
using Xunit.Abstractions;

namespace ParkingSpace.Tests.Api;

[Collection("api-context")]
public class SpotApiTests {
    private readonly ITestOutputHelper _output;
    private readonly ServiceFactory _factory;
    
    public SpotApiTests(ITestOutputHelper output, ServiceFactory factory) {
        _output = output;
        _factory = factory;
    }
    
    [Fact]
    public async Task TestApiSpotGetAll() {
        var client = _factory.CreateClient();
        var response = await client.GetFromJsonAsync<PagedResponse<List<Spot>>>($"{ServiceConstants.Root}/spot");

        _output.WriteLine(JsonSerializer.Serialize(response));
        Assert.NotNull(response!.Data);
    }
}