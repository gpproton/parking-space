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
using ParkingSpace.Helpers;
using Xunit.Abstractions;

namespace ParkingSpace.Tests.ProblemSolutions;

[TestCaseOrderer(
    ordererTypeName: "ParkingSpace.Tests.AlphabeticalOrderer",
    ordererAssemblyName: "ParkingSpace.Tests")]
[Collection("api-context")]
public class No3AirportParkingLot {
    private readonly ITestOutputHelper _output;
    private readonly IVehicleService? _vehicle;
    private readonly ISpaceService? _space;
    private readonly ISpotService? _spot;
    private readonly ITicketService? _ticket;

    public No3AirportParkingLot(ServiceFactory factory, ITestOutputHelper output) {
        _output = output;
        AsyncServiceScope scope = factory.Services.CreateAsyncScope();
        _vehicle = scope.ServiceProvider.GetService<IVehicleService>();
        _space = scope.ServiceProvider.GetService<ISpaceService>();
        _spot = scope.ServiceProvider.GetService<ISpotService>();
        _ticket = scope.ServiceProvider.GetService<ITicketService>();
    }
    
    private async Task<Space> GetSpace() =>
    (await _space!.GetByDescriptionAsync("AIRPORT")).Data!;
    
    [Fact]
    public async Task No0CreateSpots() {
        var space = await this.GetSpace();
        var spots = new List<Spot> {
            new Spot {
                Tag = "AIRPORT(Motorcycles/Scooters)",
                Active = true,
                MaximumSpot = 200,
                AvailableSpot = 200,
                VehicleType = new List<VehicleType> {
                    VehicleType.Motorcycle,
                    VehicleType.Scooter
                }
            },
            new Spot {
                Tag = "AIRPORT(Cars/SUVs)",
                Active = true,
                MaximumSpot = 500,
                AvailableSpot = 500,
                VehicleType = new List<VehicleType> {
                    VehicleType.Car,
                    VehicleType.Suv
                }
            },
            new Spot {
                Tag = "AIRPORT(Bus/Truck)",
                Active = true,
                MaximumSpot = 100,
                AvailableSpot = 100,
                VehicleType = new List<VehicleType> {
                    VehicleType.Bus,
                    VehicleType.Truck
                }
            }
        };

        foreach (var spot in spots)
            space.Spots.Add(spot);
        await _space!.UpdateAsync(space);
    }
    
    [Fact]
    public async Task No1CreateVehicles() {
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
                RegistrationNo = "motorcycle-02",
                Type = VehicleType.Motorcycle
            },
            new Vehicle {
                RegistrationNo = "car-00",
                Type = VehicleType.Car
            },
            new Vehicle {
                RegistrationNo = "car-01",
                Type = VehicleType.Car
            },
            new Vehicle {
                RegistrationNo = "suv-00",
                Type = VehicleType.Suv
            }
        });
    }
    
    [Fact]
    public async Task No2MotorcycleParked55Minutes() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddMinutes(-55);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await _ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await _ticket.UnParkVehicleAsync(park)).Data;
        
        if (ticket is null) return;
        
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Exit Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
    }
    
    [Fact]
    public async Task No3MotorcycleParked14Hours59Minutes() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("motorcycle-01");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddHours(-14).AddMinutes(-59);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await _ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await _ticket.UnParkVehicleAsync(park)).Data;
        
        if (ticket is null) return;
        
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Exit Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
    }
    
    [Fact]
    public async Task No4MotorcycleParked1Day12Hours() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("motorcycle-02");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddDays(-1).AddHours(-12);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await _ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await _ticket.UnParkVehicleAsync(park)).Data;
        
        if (ticket is null) return;
        
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Exit Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
    }
    
    [Fact]
    public async Task No5CarParked50Minutes() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("car-00");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddMinutes(-50);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await _ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await _ticket.UnParkVehicleAsync(park)).Data;
        
        if (ticket is null) return;
        
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Exit Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
    }
    
    [Fact]
    public async Task No6SuvParked23Hours59Minutes() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("suv-00");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddHours(-23).AddMinutes(-59);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await _ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await _ticket.UnParkVehicleAsync(park)).Data;
        
        if (ticket is null) return;
        
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Exit Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
    }
    
    [Fact]
    public async Task No7CarParked3Days1Hours() {
        var space = await this.GetSpace();
        var vehicle = await _vehicle!.GetByRegistrationNoAsync("car-01");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddDays(-3).AddHours(-1);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await _ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await _ticket.UnParkVehicleAsync(park)).Data;
        
        if (ticket is null) return;
        
        _output.WriteLine($@"
Parking Ticket:
==============
Vehicle: {ticket!.Vehicle!.RegistrationNo}
Ticket Number: {ticket.TicketNumber}
Spot Number: {ticket.SpotPosition}
Entry Date-time: {ticket.StartedAt}
Exit Date-time: {ticket.CompletedAt}
Fee: {ticket.Amount}
");
    }

}