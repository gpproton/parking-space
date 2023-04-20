// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ParkingSpace.Common;
using Xunit.Abstractions;

namespace ParkingSpace.Tests;

public class CustomerTests {
    private readonly ITestOutputHelper _output;
    
    public CustomerTests(ITestOutputHelper output) {
        _output = output;
    }
    
    [Fact]
    public async Task TestSample() {
        await using var application = new MinimalApplication();
        using var client = application.CreateClient();
        var response = await client.GetStringAsync($"{ServiceConstants.Root}/space");
        // var test = JsonSerializer.Deserialize<PagedResponse<List<Space>>>(response);
        _output.WriteLine(response);
        Assert.NotNull(response);
    }
}