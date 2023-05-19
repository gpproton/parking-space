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

public class No1MallParkingLot : BaseTicketTest {
    public No1MallParkingLot(ServiceFactory factory, ITestOutputHelper output) : base(factory, output) { }

    private async Task<Space> GetSpace() =>
    (await Space!.GetByDescriptionAsync("MALL")).Data!;

    [Fact]
    public async Task No0CreateSpots() {
        var space = await this.GetSpace();
        var spots = new List<Spot> {
            new Spot {
                Tag = "MALL(Motorcycles/Scooters)",
                Active = true,
                MaximumSpot = 100,
                AvailableSpot = 100,
                VehicleType = new List<VehicleType> {
                    VehicleType.Motorcycle,
                    VehicleType.Scooter
                }
            },
            new Spot {
                Tag = "MALL(Cars/SUVs)",
                Active = true,
                MaximumSpot = 80,
                AvailableSpot = 80,
                VehicleType = new List<VehicleType> {
                    VehicleType.Car,
                    VehicleType.Suv
                }
            },
            new Spot {
                Tag = "MALL(Buses/Trucks)",
                Active = true,
                MaximumSpot = 10,
                AvailableSpot = 10,
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
                RegistrationNo = "car-00",
                Type = VehicleType.Car
            },
            new Vehicle {
                RegistrationNo = "truck-00",
                Type = VehicleType.Truck
            }
        });
    }

    [Fact]
    public async Task No2MotorcycleParked3Hours30Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-00");

        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddHours(-3).AddMinutes(-30);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;

        if (park is null) return;
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(40, ticket!.Amount);
    }

    [Fact]
    public async Task No3CarParked6Hours1Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("car-00");

        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddHours(-6).AddMinutes(-1);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;

        if (park is null) return;
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(140, ticket!.Amount);
    }

    [Fact]
    public async Task No4TruckParked1Hours59Minutes() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("truck-00");

        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Now.AddHours(-1).AddMinutes(-59);
        var options = new SpotVehicleParams(space, vehicle.Data, time);
        var park = (await Ticket!.ParkVehicleAsync(options)).Data;

        if (park is null) return;
        park.CompletedAt = DateTimeOffset.Now;
        var ticket = (await Ticket.UnParkVehicleAsync(park)).Data;

        if (ticket is null) return;
        this.PrintTicket(ticket);
        Assert.Equal(100, ticket!.Amount);
    }
}