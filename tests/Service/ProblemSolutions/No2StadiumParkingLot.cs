// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ParkingSpace.Enums;
using ParkingSpace.Features.Space.Entities;
using ParkingSpace.Features.Vehicle.Entities;
using ParkingSpace.Helpers;
using ParkingSpace.Tests.Shared;
using Xunit.Abstractions;

namespace ParkingSpace.Tests.ProblemSolutions;

public class No2StadiumParkingLot : BaseTicketTest {
    public No2StadiumParkingLot(ServiceFactory factory, ITestOutputHelper output) : base(factory, output) { }
    
    private async Task<Space> GetSpace() =>
    (await Space!.GetByDescriptionAsync("STADIUM")).Data!;
    
    [Fact]
    public async Task No0CreateSpots() {
        var space = await this.GetSpace();
        var spots = new List<Spot> {
            new Spot {
                Tag = "STADIUM(Motorcycles/Scooters)",
                Active = true,
                MaximumSpot = 1000,
                AvailableSpot = 1000,
                VehicleType = new List<VehicleType> {
                    VehicleType.Motorcycle,
                    VehicleType.Scooter
                }
            },
            new Spot {
                Tag = "STADIUM(Cars/SUVs)",
                Active = true,
                MaximumSpot = 1500,
                AvailableSpot = 1500,
                VehicleType = new List<VehicleType> {
                    VehicleType.Car,
                    VehicleType.Suv
                }
            }
        };

        foreach (var spot in spots)
            space.Spots.Add(spot);
        await Space!.UpdateAsync(space);
    }
    
    [Fact]
    public async Task No1CreateVehicles() {
        await Vehicle!.AddRangeAsync(new List<Vehicle> {
            new Vehicle {
                RegistrationNo = "motorcycle-00",
                Type = VehicleType.Motorcycle
            },
            new Vehicle {
                RegistrationNo = "motorcycle-01",
                Type = VehicleType.Motorcycle
            },
            new Vehicle {
                RegistrationNo = "suv-electric-00",
                Type = VehicleType.Suv
            },
            new Vehicle {
                RegistrationNo = "suv-00",
                Type = VehicleType.Suv
            }
        });
    }
    
    [Fact]
    public async Task No2MotorcycleParked3Hours40Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddHours(-3).AddMinutes(-40);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        
        this.PrintTicket(ticket);
        Assert.Equal(30, ticket!.Amount);
    }
    
    [Fact]
    public async Task No3MotorcycleParked14Hours59Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-01");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddHours(-14).AddMinutes(-59);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        
        this.PrintTicket(ticket);
        Assert.Equal(390, ticket!.Amount);
    }
    
    [Fact]
    public async Task No4ElectricSuvParked11Hours30Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("suv-electric-00");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddHours(-11).AddMinutes(-30);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        
        this.PrintTicket(ticket);
        Assert.Equal(180, ticket!.Amount);
    }
    
    [Fact]
    public async Task No5SuvParked13Hours5Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("suv-electric-00");
        if (vehicle.Data is null) return;
        
        var time = DateTimeOffset.Now.AddHours(-13).AddMinutes(-5);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;
        
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        
        this.PrintTicket(ticket);
        Assert.Equal(580, ticket!.Amount);
    }
}