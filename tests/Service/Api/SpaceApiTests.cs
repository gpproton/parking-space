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
using System.Text;
using System.Text.Json;
using ParkingSpace.Common;
using ParkingSpace.Common.Response;
using ParkingSpace.Features.Space.Entities;
using Xunit.Abstractions;

namespace ParkingSpace.Tests.Api;

[Collection("api-context")]
public class SpaceApiTests {
    private readonly ITestOutputHelper _output;
    private readonly ServiceFactory _factory;

    public SpaceApiTests(ITestOutputHelper output, ServiceFactory factory) {
        _output = output;
        _factory = factory;
    }

    [Fact]
    public async Task TestApiSpaceGetAll() {
        var client = _factory.CreateClient();
        var response = await client.GetFromJsonAsync<PagedResponse<List<Space>>>($"{ServiceConstants.Root}/space");

        _output.WriteLine(JsonSerializer.Serialize(response));
        Assert.NotNull(response);
    }

    [Fact]
    public async Task TestApiSpaceCreate() {
        var client = _factory.CreateClient();
        var space = new Space { Description = "Test-01", Active = true };
        var content = new StringContent(JsonSerializer.Serialize(space), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(new Uri($"{ServiceConstants.Root}/space"), content);
        var value = await response.Content.ReadFromJsonAsync<Response<Space>>();

        _output.WriteLine(JsonSerializer.Serialize(value));
        Assert.NotNull(value);
    }
}