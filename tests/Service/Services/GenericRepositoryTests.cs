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
using ParkingSpace.Common.Entity;
using ParkingSpace.Features.Customer.Entities;
using ParkingSpace.Filters;

namespace ParkingSpace.Tests.Services;

[TestCaseOrderer(
    ordererTypeName: "ParkingSpace.Tests.AlphabeticalOrderer",
    ordererAssemblyName: "ParkingSpace.Tests")]
[Collection("api-context")]
public class GenericRepositoryTests {
    private readonly AsyncServiceScope _scope;
    private readonly IRepository<Customer>? _service;

    public GenericRepositoryTests(ServiceFactory factory) {
        _scope = factory.Services.CreateAsyncScope();
        _service = _scope.ServiceProvider.GetService<IRepository<Customer>>();
    }

    private static readonly Customer SingleSample = new Customer {
        Id = Guid.NewGuid(),
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@mail.xx",
        Phone = "+1303930333"
    };

    [Fact]
    public async Task GenericService0AddAsyncTest() {
        var result = await _service!.AddAsync(SingleSample);

        Assert.NotNull(result);
        Assert.Equal(SingleSample.Id, result.Id);
    }

    [Fact]
    public async Task GenericService1GetByIdAsyncTest() {
        var result = await _service!.GetByIdAsync(SingleSample.Id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GenericService2GetAllTest() {
        var result = await _service!.GetAllAsync(new PageFilter());

        Assert.IsAssignableFrom<IEnumerable<Customer>>(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GenericService3UpdateAsyncTest() {
        SingleSample.FirstName = "Jane";
        await _service!.UpdateAsync(SingleSample);
        var result = await _service!.GetByIdAsync(SingleSample.Id);

        Assert.Equal(result!.FirstName, SingleSample.FirstName);
    }

    [Fact]
    public async Task GenericService4ArchiveAsyncTest() {
        await _service!.ArchiveAsync(SingleSample);
        var result = await _service!.GetByIdAsync(SingleSample.Id);

        Assert.Null(result);
    }

    private static readonly List<Customer> MultipleSample = new List<Customer> {
        new Customer {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@mail.xx",
            Phone = "+1303930333"
        },
        new Customer {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@mail.xx",
            Phone = "+130344443"
        }
    };

    [Fact]
    public async Task GenericService5AddRangeAsyncTest() {
        var result = await _service!.AddRangeAsync(MultipleSample);

        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GenericService6UpdateRangeAsyncTest() {
        MultipleSample[0].FirstName = "JohnX";
        await _service!.UpdateRangeAsync(MultipleSample);
        var result = await _service!.GetAllAsync(new PageFilter());

        IEnumerable<Customer> collection = result.ToList();
        Assert.NotEmpty(collection);
        var data = collection.Select(x => x.FirstName).FirstOrDefault(x => x == "JohnX");
        Assert.Equal(MultipleSample[0].FirstName, data);
    }

    [Fact]
    public async Task GenericService7ArchiveRangeAsyncTest() {
        await _service!.ArchiveRangeAsync(MultipleSample);
        var result = await _service!.GetAllAsync(new PageFilter());

        Assert.Empty(result);
    }
}