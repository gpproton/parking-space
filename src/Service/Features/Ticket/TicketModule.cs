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

namespace ParkingSpace.Features.Ticket;

public class TicketModule : IModule {

public IServiceCollection RegisterApiModule(IServiceCollection services) {
    services.AddScoped<ITicketService, TicketService>();

    return services;
}
public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
    const string name = nameof(Ticket);
    var url = $"{ServiceConstants.Root}/{name.ToLower()}";
        
    endpoints.MapGet(url, (PageFilter? filter, [FromServices] ITicketService service) =>
    service.GetAllAsync(filter)
    ).WithName($"Get{name}")
    .WithTags(name);
    
    endpoints.MapGet($"{url}/pending", (PageFilter? filter, [FromServices] ITicketService service) =>
    service.GetAllAsync(filter)
    ).WithName($"GetPending{name}")
    .WithTags(name);
        
    endpoints.MapGet($"{url}/:id", ([FromServices] ITicketService service, Guid id) =>
    service.GetByIdAsync(id)
    ).WithName($"Get{name}ById")
    .WithTags(name);
        
    endpoints.MapPost(url, ([FromServices] ITicketService service, [FromBody] Entities.Ticket item) =>
    service.AddAsync(item)        
    ).WithName($"Create{name}")
    .WithTags(name);
        
    endpoints.MapPut(url, ([FromServices] ITicketService service, [FromBody] Entities.Ticket item) =>
    service.UpdateAsync(item)        
    ).WithName($"Update{name}")
    .WithTags(name);
        
    endpoints.MapDelete($"{url}/:id", ([FromServices] ITicketService service, Guid id) =>
    service.ArchiveAsync(id)        
    ).WithName($"Cancel{name}")
    .WithTags(name);

    return endpoints;
}
}