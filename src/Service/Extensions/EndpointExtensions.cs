// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ParkingSpace.Common.Helpers;
using ParkingSpace.Interfaces;

namespace ParkingSpace.Extensions;

public static class EndpointExtensions {
    private static readonly List<IModule> RegisteredModules = new List<IModule>();

    public static IServiceCollection RegisterModules(this IServiceCollection services) {
        var modules = DiscoverModules();
        foreach (var module in modules) {
            module.RegisterApiModule(services);
            RegisteredModules.Add(module);
        }

        return services;
    }

    public static WebApplication RegisterApiEndpoints(this WebApplication app) {
        foreach (var module in RegisteredModules)
            module.MapEndpoints(app);

        return app;
    }

    private static IEnumerable<IModule> DiscoverModules()
    => FactoryLoader.LoadClassInstances<IModule>();
}