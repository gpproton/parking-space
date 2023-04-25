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
using ParkingSpace.Interfaces;

namespace ParkingSpace.Features.Custom;

public class CustomModule : IModule {

    public IServiceCollection RegisterApiModule(IServiceCollection services) {
        return services;
    }
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        const string name = "shared";
        var url = $"{ServiceConstants.Root}/{name}";

        endpoints.MapGet($"{url}/test", () => {

        }
        ).WithName($"Get{name}")
        .WithTags(name);

        return endpoints;
    }
}