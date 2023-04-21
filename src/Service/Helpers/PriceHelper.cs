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
using ParkingSpace.Features.Ticket.Entities;

namespace ParkingSpace.Helpers;

public static class PriceHelper {
    public static double CalculatePrice(Ticket ticket, List<Price> prices) {
        var totalTimeSpent = ((DateTimeOffset.Now - ticket.StartedAt)!).Value.TotalHours;
        var totalDays = totalTimeSpent / 24;
        double accumulatedTime = 0;
        double accumulatedCharge = 0;

        var flatRatePrices = prices.Where(x => x.ChargeModel.Equals(PriceModel.FlatRate)).OrderBy(x => x.MaximumTime);
        var hourlyRatePrices = prices.FirstOrDefault(x => x.ChargeModel.Equals(PriceModel.PerInfinityHour));
        var dayRatePrices = prices.FirstOrDefault(x => x.ChargeModel.Equals(PriceModel.PerDay));

        foreach (var price in flatRatePrices.Select((value, index) =>
                 new { Value = value, Index = index })) {
            // Handle summed rates
            if (accumulatedTime <= totalTimeSpent && price.Value.MaximumTime < totalTimeSpent) {
                var temp = (price.Value.MaximumTime - accumulatedTime) * 1;
                accumulatedTime += temp > accumulatedTime ? temp : accumulatedCharge;
                accumulatedCharge += price.Value.Amount;
            }

            // Handles Flat rate up to one day.
            var lastIndex = price.Index == flatRatePrices.Count() - 1;
            if (!price.Value.SumPrice && lastIndex) {
                accumulatedCharge = price.Value.Amount;
                break;
            }
        }

        // Handles hourly rates
        if (hourlyRatePrices is not null)
            accumulatedCharge += Math.Ceiling(totalTimeSpent - accumulatedTime) * hourlyRatePrices.Amount;

        // Handles daily rates
        if (dayRatePrices is not null && totalDays >= 1)
            return Math.Ceiling(totalDays) * dayRatePrices.Amount;

        return accumulatedCharge;
    }
}