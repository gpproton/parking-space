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
using ParkingSpace.Features.Space;
using ParkingSpace.Features.Ticket;
using ParkingSpace.Features.Ticket.Entities;
using ParkingSpace.Features.Vehicle;
using Xunit.Abstractions;

namespace ParkingSpace.Tests.Shared;

[TestCaseOrderer(
    ordererTypeName: "ParkingSpace.Tests.AlphabeticalOrderer",
    ordererAssemblyName: "ParkingSpace.Tests")]
[Collection("api-context")]
public abstract class BaseTicketTest {
    protected readonly ITestOutputHelper Output;
    protected readonly IVehicleService? Vehicle;
    protected readonly ISpaceService? Space;
    protected readonly ISpotService? Spot;
    protected readonly ITicketService? Ticket;

    protected BaseTicketTest(ServiceFactory factory, ITestOutputHelper output) {
        Output = output;
        AsyncServiceScope scope = factory.Services.CreateAsyncScope();
        Vehicle = scope.ServiceProvider.GetService<IVehicleService>();
        Space = scope.ServiceProvider.GetService<ISpaceService>();
        Spot = scope.ServiceProvider.GetService<ISpotService>();
        Ticket = scope.ServiceProvider.GetService<ITicketService>();
    }

    protected void PrintTicket(Ticket? ticket) {
        if (ticket is null) return;
        Output.WriteLine($@"
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
    public async Task No99ClearData() {
        await Ticket!.ClearAsync();
        await Vehicle!.ClearAsync();
        await Spot!.ClearAsync();
    }
}