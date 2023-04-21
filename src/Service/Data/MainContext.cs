// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.EntityFrameworkCore;
using ParkingSpace.Common.Interfaces;
using ParkingSpace.Features.Customer.Entities;
using ParkingSpace.Features.Incident.Entities;
using ParkingSpace.Features.Price.Entities;
using ParkingSpace.Features.Space.Entities;
using ParkingSpace.Features.Staff.Entities;
using ParkingSpace.Features.Ticket.Entities;
using ParkingSpace.Features.Vehicle.Entities;

namespace ParkingSpace.Data;

public class MainContext : DbContext {
    public MainContext(DbContextOptions<MainContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) { }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Incident> Incidents { get; set; } = null!;
    public DbSet<Price> Prices { get; set; } = null!;
    public DbSet<Space> Spaces { get; set; } = null!;
    public DbSet<Spot> Spots { get; set; } = null!;
    public DbSet<Staff> Staffs { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
}