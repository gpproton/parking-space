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


public class No3AirportParkingLot : BaseTicketTest {
    public No3AirportParkingLot(ServiceFactory factory, ITestOutputHelper output) : base(factory, output) { }

    private async Task<Space> GetSpace() =>
    (await Space!.GetByDescriptionAsync("AIRPORT")).Data!;

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
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddMinutes(-55);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;

        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(0, ticket!.Amount);
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
        Assert.Equal(60, ticket!.Amount);
    }

    [Fact]
    public async Task No4MotorcycleParked1Day12Hours() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-02");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddDays(-1).AddHours(-12);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;

        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(160, ticket!.Amount);
    }

    [Fact]
    public async Task No5CarParked50Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("car-00");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddMinutes(-50);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;

        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(60, ticket!.Amount);
    }

    [Fact]
    public async Task No6SuvParked23Hours59Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("suv-00");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddHours(-23).AddMinutes(-59);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;

        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(80, ticket!.Amount);
    }

    [Fact]
    public async Task No7CarParked3Days1Hours() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("car-01");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddDays(-3).AddHours(-1);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;
        if (park is null) return;

        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(400, ticket!.Amount);
    }
}