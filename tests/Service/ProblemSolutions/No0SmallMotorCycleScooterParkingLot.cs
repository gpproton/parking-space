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

public class No0SmallMotorCycleScooterParkingLot : BaseTicketTest {
    public No0SmallMotorCycleScooterParkingLot(ServiceFactory factory, ITestOutputHelper output) : base(factory, output) { }

    private async Task<Space> GetSpace() =>
    (await Space!.GetByDescriptionAsync("MALL")).Data!;

    [Fact]
    public async Task No0CreateSpots() {
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
                RegistrationNo = "scooter-00",
                Type = VehicleType.Scooter
            },
            new Vehicle {
                RegistrationNo = "scooter-01",
                Type = VehicleType.Scooter
            }
        });
    }

    [Fact]
    public async Task No2ParkMotoCycle00Test() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Parse("29-May-2022 14:04:07");
        var option = new SpotVehicleParams(space, vehicle.Data, time);
        var ticket = (await Ticket!.ParkVehicleAsync(option)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(1, ticket!.SpotPosition);
    }

    [Fact]
    public async Task No3ParkScooter00Test() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("scooter-00");
        if (vehicle.Data is null) return;

        var time = DateTimeOffset.Parse("29-May-2022 14:44:07");
        var option = new SpotVehicleParams(space, vehicle.Data, time);
        var ticket = (await Ticket!.ParkVehicleAsync(option)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(2, ticket!.SpotPosition);
    }

    [Fact]
    public async Task No4NoSpaceAvailableScooter01Test() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("scooter-01");

        if (vehicle.Data is null) return;
        var time = DateTimeOffset.Parse("29-May-2022 14:53:07");
        var option = new SpotVehicleParams(space, vehicle.Data, time);
        var ticket = await Ticket!.ParkVehicleAsync(option);

        Output.WriteLine(ticket.Message);
        Assert.Equal("No space available", ticket.Message);

    }

    [Fact]
    public async Task No5UnParkScooter00Test() {
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("scooter-00");
        if (vehicle.Data is null) return;

        var pending = (await Ticket!.GetActiveByVehicleAsync(vehicle.Data)).Data;
        pending!.CompletedAt = DateTimeOffset.Parse("29-May-2022 15:40:07");
        var ticket = (await Ticket.UnParkVehicleAsync(pending)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(10, ticket!.Amount);
    }

    [Fact]
    public async Task No6ParkMotoCycle01Test() {
        var space = await this.GetSpace();
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-01");
        if (vehicle.Data is null) return;
        var time = DateTimeOffset.Parse("29-May-2022 15:59:07");
        var option = new SpotVehicleParams(space, vehicle.Data, time);
        var ticket = (await Ticket!.ParkVehicleAsync(option)).Data;
        this.PrintTicket(ticket);
        Assert.Equal(2, ticket!.SpotPosition);
    }

    [Fact]
    public async Task No6UnParkMotorcycle00Test() {
        var vehicle = await Vehicle!.GetByRegistrationNoAsync("motorcycle-00");
        if (vehicle.Data is not null) {
            var pending = (await Ticket!.GetActiveByVehicleAsync(vehicle.Data)).Data;
            pending!.CompletedAt = DateTimeOffset.Parse("29-May-2022 17:44:07");
            var ticket = (await Ticket.UnParkVehicleAsync(pending)).Data;
            this.PrintTicket(ticket);
            Assert.Equal(40, ticket!.Amount);
        }
    }
}