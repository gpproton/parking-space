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
using ParkingSpace.Filters;
using ParkingSpace.Interfaces;

namespace ParkingSpace.Features.Space;

public class SpaceModule : IModule {

    public IServiceCollection RegisterApiModule(IServiceCollection services) {
        services.AddScoped<ISpaceService, SpaceService>();

        return services;
    }
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        
        const string name = nameof(Space);
        var url = $"{ServiceConstants.Root}/{name.ToLower()}";
        
        endpoints.MapGet(url, (PageFilter? filter, [FromServices] ISpaceService service) =>
            service.GetAllAsync(filter)
        ).WithName("GetSpace")
        .WithTags(name);
        
        endpoints.MapGet($"{url}/:id", ([FromServices] ISpaceService service, Guid id) =>
        service.GetByIdAsync(id)
        ).WithName("GetSpaceById")
        .WithTags(name);
        
        endpoints.MapPost(url, ([FromServices] ISpaceService service, [FromBody] Entities.Space item) =>
        service.AddAsync(item)        
        ).WithName("CreateSpace")
        .WithTags(name);
        
        endpoints.MapPut(url, ([FromServices] ISpaceService service, [FromBody] Entities.Space item) =>
        service.UpdateAsync(item)        
        ).WithName("UpdateSpace")
        .WithTags(name);
        
        endpoints.MapDelete($"{url}/:id", ([FromServices] ISpaceService service, Guid id) =>
        service.ArchiveAsync(id)        
        ).WithName("ArchiveSpace")
        .WithTags(name);

        return endpoints;
    }
}