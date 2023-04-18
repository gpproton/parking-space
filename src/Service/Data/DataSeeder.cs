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
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Data;

public class DataSeeder {
    private readonly ModelBuilder _builder;
    
    public DataSeeder(ModelBuilder builder) => _builder = builder;

    public void Run() {
        var time = DateTimeOffset.Now;

        var space = new List<Space> {
            new Space {
                Id = Guid.NewGuid(),
                Active = true,
                Description = "Mall"
            },
            new Space {
                Id = Guid.NewGuid(),
                Active = true,
                Description = "Stadium"
            },
            new Space {
                Id = Guid.NewGuid(),
                Active = true,
                Description = "Airport"
            }
        };

        _builder.Entity<Space>().HasData(space);
    }
}