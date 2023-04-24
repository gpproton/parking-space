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
using ParkingSpace.Enums;
using ParkingSpace.Features.Space;
using ParkingSpace.Features.Space.Entities;
using ParkingSpace.Features.Ticket;
using ParkingSpace.Features.Vehicle;
using ParkingSpace.Features.Vehicle.Entities;
using ParkingSpace.Filters;
using ParkingSpace.Helpers;
using Xunit.Abstractions;

namespace ParkingSpace.Tests.ProblemSolutions;

[TestCaseOrderer(
    ordererTypeName: "ParkingSpace.Tests.AlphabeticalOrderer",
    ordererAssemblyName: "ParkingSpace.Tests")]
[Collection("api-context")]
public class SmallMotorCycleScooterParkingLot {
    private readonly ITestOutputHelper _output;
    private readonly IVehicleService? _vehicle;
    private readonly ISpaceService? _space;
    private readonly ISpotService? _spot;
    private readonly ITicketService? _ticket;

    public SmallMotorCycleScooterParkingLot(ServiceFactory factory, ITestOutputHelper output) {
        _output = output;
        AsyncServiceScope scope = factory.Services.CreateAsyncScope();
        _vehicle = scope.ServiceProvider.GetService<IVehicleService>();
        _space = scope.ServiceProvider.GetService<ISpaceService>();
        _spot = scope.ServiceProvider.GetService<ISpotService>();
        _ticket = scope.ServiceProvider.GetService<ITicketService>();
    }
    
    private async Task<Space> GetSpace() =>
    (await _space!.GetByDescriptionAsync("MALL")).Data!;

    [Fact]
    public async Task Solution0CreateSpotsTest() {
        var space = await this.GetSpace();
        space.Spots.Add(new Spot {
            Tag = "MALL(Motorcycles/Scooters)",
            Active = true,
            MaximumSpot = 2,
            AvailableSpot = 2,
            VehicleType = new List<VehicleType> {
                VehicleType.Motorcycle,
                VehicleType.Scooter
            }
        });
        await _space!.UpdateAsync(space);
        var spot = _spot!.GetByTagAsync("MALL(Motorcycles/Scooters)");
        
        Assert.NotNull(spot);
    }

    [Fact]
    public async Task Solution1CreateVehiclesTest() {
        await _vehicle!.AddRangeAsync(new List<Vehicle> {
            new Vehicle {
                RegistrationNo = "motorcycle-00",
                Type = VehicleType.Motorcycle
            },
            new Vehicle {
                RegistrationNo = "motorcycle-01",
                Type = VehicleType.Motorcycle
            },
            new Vehicle {
                RegistrationNo = "scooter-00",
                Type = VehicleType.Scooter
            },
            new Vehicle {
                RegistrationNo = "scooter-01",
                Type = VehicleType.Scooter
            }
        });
        var vehicles = await _vehicle.GetAllAsync(new PageFilter());
        Assert.NotEmpty(vehicles.Data!);
    }
    
    [Fact]
    public async Task Solution2ParkMotoCycle00Test() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is not null) {
            var ticket = (await _ticket!.ParkVehicleAsync(new SpotVehicleParams(
                     space, 
                     vehicle.Data, 
                     DateTimeOffset.Parse("29-May-2022 14:04:07")))
              )
        .Data;
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
");
        Assert.Equal(1, ticket.SpotPosition);
        }
    }

    [Fact]
    public async Task Solution3ParkScooter00Test() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("scooter-00");

        if (vehicle.Data is not null) {
            var ticket = (await _ticket!.ParkVehicleAsync(new SpotVehicleParams(
                              space,
                              vehicle.Data,
                              DateTimeOffset.Parse("29-May-2022 14:44:07")))
                         ).Data;
            _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
");
            Assert.Equal(2, ticket.SpotPosition);
        }
    }
    
    [Fact]
    public async Task Solution4NoSpaceAvailableScooter01Test() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("scooter-01");

        if (vehicle.Data is not null) {
            var ticket = (await _ticket!.ParkVehicleAsync(
                     new SpotVehicleParams(
                              space,
                              vehicle.Data,
                              DateTimeOffset.Parse("29-May-2022 14:53:07")))
             );
            
            _output.WriteLine(ticket.Message);
            Assert.Equal("No space available", ticket.Message);
        }
    }

    [Fact]
    public async Task Solution5UnParkScooter00Test() {
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("scooter-00");
        if (vehicle.Data is not null) {
            var pending = (await _ticket!.GetActiveByVehicleAsync(vehicle.Data)).Data;
            pending!.CompletedAt = DateTimeOffset.Parse("29-May-2022 15:40:07");
            var ticket = (await _ticket.UnParkVehicleAsync(pending)).Data;
            if (ticket is not null) {
                _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Entry Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
            }
        }
    }
    
    [Fact]
    public async Task Solution6ParkMotoCycle01Test() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("motorcycle-01");
        if (vehicle.Data is not null) {
            var ticket = (await _ticket!.ParkVehicleAsync(new SpotVehicleParams(
                              space, 
                              vehicle.Data, 
                              DateTimeOffset.Parse("29-May-2022 15:59:07")))
                         )
            .Data;
            _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
");
            Assert.Equal(2, ticket.SpotPosition);
        }
    }
    
    [Fact]
    public async Task Solution6UnParkMotorcycle00Test() {
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is not null) {
            var pending = (await _ticket!.GetActiveByVehicleAsync(vehicle.Data)).Data;
            pending!.CompletedAt = DateTimeOffset.Parse("29-May-2022 17:44:07");
            var ticket = (await _ticket.UnParkVehicleAsync(pending)).Data;
            if (ticket is not null) {
                _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Entry Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
            }
        }
    }
}