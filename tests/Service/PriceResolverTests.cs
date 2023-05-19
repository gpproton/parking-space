// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpace.Data.Seeds;
using ParkingSpace.Enums;
using ParkingSpace.Features.Space.Entities;
using ParkingSpace.Features.Ticket.Entities;
using ParkingSpace.Features.Vehicle.Entities;
using ParkingSpace.Helpers;

namespace ParkingSpace.Tests;

public class PriceResolverTests {
    [Fact]
    public async Task SimpleStadiumMotorcycleTest() {
        var spaces = SpaceSeeds.GetSpaces().FirstOrDefault(x => x.Description.Equals("STADIUM"));
        var vehicle = new Vehicle {
            RegistrationNo = "XXX 123 XX",
            Type = VehicleType.Motorcycle
        };

        var ticket = new Ticket {
            TicketNumber = Guid.NewGuid().ToString(),
            Vehicle = vehicle,
            StartedAt = DateTimeOffset.Now.AddHours(-3).AddMinutes(-40),
            CompletedAt = DateTimeOffset.Now,
            Spot = new Spot { Tag = "XXX-001" }
        };

        var prices = spaces!.Prices.Where(x => x.VehicleType.Contains(vehicle.Type)).ToList();
        var amount = PriceResolver.CalculatePrice(ticket, prices);

        Assert.Equal(30, amount);
        await Task.Delay(5);
    }
}