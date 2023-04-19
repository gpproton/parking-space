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

namespace ParkingSpace.Features.Customer;

public class CustomerModule : IModule {
    public IServiceCollection RegisterApiModule(IServiceCollection services) {
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
    
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        const string name = nameof(Entities.Customer);
        var url = $"{ServiceConstants.Root}/{name.ToLower()}";
        
        endpoints.MapGet(url, (PageFilter? filter, [FromServices] ICustomerService service) =>
        service.GetAllAsync(filter)
        ).WithName($"Get{name}")
        .WithTags(name);
        
        endpoints.MapGet($"{url}/:id", ([FromServices] ICustomerService service, Guid id) =>
        service.GetByIdAsync(id)
        ).WithName($"Get{name}ById")
        .WithTags(name);
        
        endpoints.MapPost(url, ([FromServices] ICustomerService service, [FromBody] Entities.Customer item) =>
        service.AddAsync(item)        
        ).WithName($"Create{name}")
        .WithTags(name);
        
        endpoints.MapPut(url, ([FromServices] ICustomerService service, [FromBody] Entities.Customer item) =>
        service.UpdateAsync(item)        
        ).WithName($"Update{name}")
        .WithTags(name);
        
        endpoints.MapDelete($"{url}/:id", ([FromServices] ICustomerService service, Guid id) =>
        service.ArchiveAsync(id)        
        ).WithName($"Archive{name}")
        .WithTags(name);

        return endpoints;
    }
}