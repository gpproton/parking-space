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
using ParkingSpace.Features.Price.Entities;
using ParkingSpace.Features.Space.Entities;

namespace ParkingSpace.Data.Seeds {
    public static class SpaceSeeds {
        public static IEnumerable<Space> GetSpaces() {
            return new List<Space> {
            // MALL spots & prices
            new Space {
                Active = true,
                Description = "MALL",
                Prices = new List<Price> {
                    new Price {
                        Description = "MALL(Motorcycles/Scooters)",
                        ChargeModel = PriceModel.PerInfinityHour,
                        Amount = 10,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle,
                            VehicleType.Scooter
                        }
                    },
                    new Price {
                        Description = "MALL(Car/SUV)",
                        ChargeModel = PriceModel.PerInfinityHour,
                        Amount = 20,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    },
                    new Price {
                        Description = "MALL(Bus/Truck)",
                        ChargeModel = PriceModel.PerInfinityHour,
                        Amount = 50,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Bus,
                            VehicleType.Truck
                        },
                    }
                },
                // Spots = new List<Spot> {
                //     new Spot {
                //         Tag = "MALL(Bus/Truck)",
                //         Active = true,
                //         VehicleType = new List<VehicleType> {
                //             VehicleType.Bus,
                //             VehicleType.Truck
                //         }
                //     }
                // }
            },
            // STADIUM spots & prices
            new Space {
                Active = true,
                Description = "STADIUM",
                Prices = new List<Price> {
                    // Stadium Motorcycle prices
                    new Price {
                        Description = "STADIUM(Motorcycles/Scooters) - 4 Hours",
                        ChargeModel = PriceModel.FlatRate,
                        MaximumTime = 4,
                        Amount = 30,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle,
                            VehicleType.Scooter
                        }
                    },
                    new Price {
                        Description = "STADIUM(Motorcycles/Scooters) - 12 Hours",
                        ChargeModel = PriceModel.FlatRate,
                        MaximumTime = 12,
                        Amount = 60,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle,
                            VehicleType.Scooter
                        }
                    },
                    new Price {
                        Description = "STADIUM(Motorcycles/Scooters) - Above 12 Hours",
                        ChargeModel = PriceModel.PerInfinityHour,
                        MaximumTime = 12,
                        Amount = 100,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle,
                            VehicleType.Scooter
                        }
                    },
                    // Stadium Car/SUV prices
                    new Price {
                        Description = "STADIUM(Car/SUV) - 4 Hours",
                        MaximumTime = 4,
                        Amount = 60,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    },
                    new Price {
                        Description = "STADIUM(Car/SUV) - 12 Hours",
                        MaximumTime = 12,
                        Amount = 120,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    },
                    new Price {
                        Description = "STADIUM(Car/SUV) - Above 12 Hours",
                        ChargeModel = PriceModel.PerInfinityHour,
                        MaximumTime = 12,
                        Amount = 200,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    }
                }
            },
            // AIRPORT spots & prices
            new Space {
                Active = true,
                Description = "AIRPORT",
                Prices = new List<Price> {
                    // Stadium Motorcycle prices
                    new Price {
                        Description = "AIRPORT(Motorcycle) - 1 Hours",
                        MaximumTime = 1,
                        Amount = 0,
                        SumPrice = false,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle
                        }
                    },
                    new Price {
                        Description = "AIRPORT(Motorcycle) - 8 Hours",
                        MaximumTime = 8,
                        Amount = 40,
                        SumPrice = false,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle
                        }
                    },
                    new Price {
                        Description = "AIRPORT(Motorcycle) - 24 Hours",
                        MaximumTime = 24,
                        Amount = 60,
                        SumPrice = false,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle
                        }
                    },
                    new Price {
                        Description = "AIRPORT(Motorcycle) - Per day",
                        MaximumTime = 24,
                        Amount = 80,
                        ChargeModel = PriceModel.PerDay,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Motorcycle
                        }
                    },
                    // Stadium Car/SUV prices
                    new Price {
                        Description = "AIRPORT(Car/SUV) - 12 Hours",
                        MaximumTime = 12,
                        Amount = 60,
                        SumPrice = false,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    },
                    new Price {
                        Description = "AIRPORT(Car/SUV) - 24 Hours",
                        MaximumTime = 24,
                        Amount = 80,
                        SumPrice = false,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    },
                    new Price {
                        Description = "AIRPORT(Car/SUV) - Per day",
                        MaximumTime = 24,
                        Amount = 100,
                        ChargeModel = PriceModel.PerDay,
                        VehicleType = new List<VehicleType> {
                            VehicleType.Car,
                            VehicleType.Suv
                        }
                    }
                }
            }
        };
        }
    }
}