// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.AspNetCore.Mvc;
using ParkingSpace.Common;
using ParkingSpace.Features.Space.Entities;
using ParkingSpace.Filters;
using ParkingSpace.Helpers;
using ParkingSpace.Interfaces;

namespace ParkingSpace.Features.Space;

public class SpotModule : IModule {
    public IServiceCollection RegisterApiModule(IServiceCollection services) {
        services.AddScoped<ISpotService, SpotService>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        const string name = nameof(Spot);
        var url = $"{ServiceConstants.Root}/{name.ToLower()}";

        endpoints.MapGet(url, async (PageFilter? filter, [FromServices] ISpotService service) => await service.GetAllAsync(filter)
        ).WithName($"Get{name}")
        .WithTags(name);

        endpoints.MapGet($"{url}/:id", async ([FromServices] ISpotService service, Guid id) => {
            var result = await service.GetByIdAsync(id);
            return result.Success ? Results.Ok(result) : Results.NotFound();
        }
        ).WithName($"Get{name}ById")
        .WithTags(name);

        endpoints.MapPost(url, async ([FromServices] ISpotService service, [FromBody] Spot item) =>
        await service.AddAsync(item)
        ).WithName($"Create{name}")
        .WithTags(name);

        endpoints.MapPut(url, async ([FromServices] ISpotService service, [FromBody] Spot item) =>
        await service.UpdateAsync(item)
        ).WithName($"Update{name}")
        .WithTags(name);

        endpoints.MapDelete($"{url}/:id", async ([FromServices] ISpotService service, Guid id) =>
        await service.ArchiveAsync(id)
        ).WithName($"Archive{name}")
        .WithTags(name);

        endpoints.MapGet($"{url}/tag", async (
            [FromServices] ISpotService service,
            [FromQuery] string tag) =>
            await service.GetByTagAsync(tag)
        ).WithName($"GetBy{name}Tag")
        .WithTags(name);

        endpoints.MapPost($"{url}/check/vehicle", async (
                [FromServices] ISpotService service,
                [FromBody] SpotVehicleParams option) =>
            await service.CheckAvailabilityAsync(option)
        ).WithName($"Check{name}ByVehicle")
        .WithTags(name);

        endpoints.MapPost($"{url}/check/vehicle-type", async (
                [FromServices] ISpotService service,
                [FromBody] SpotVehicleTypeParams option) =>
            await service.GetByVehicleTypeAsync(option)
        ).WithName($"Check{name}ByVehicleType")
        .WithTags(name);

        return endpoints;
    }
}