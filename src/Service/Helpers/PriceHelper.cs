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
        double totalTimeSpent = ((ticket.CompletedAt - ticket.StartedAt)!).Value.TotalHours;
        double accumulatedTime = 0;
        double accumulatedCharge = 0;
        double totalDaysSpent = totalTimeSpent / 24;
        
        // Rate filters
        var flatRatePrices = prices.Where(x => x.ChargeModel.Equals(PriceModel.FlatRate)).OrderBy(x => x.MaximumTime);
        var hourlyRatePrices = prices.FirstOrDefault(x => x.ChargeModel.Equals(PriceModel.PerInfinityHour));
        var dayRatePrices = prices.FirstOrDefault(x => x.ChargeModel.Equals(PriceModel.PerDay));

        foreach (var price in flatRatePrices) {
            if (accumulatedTime >= totalTimeSpent) break;
            accumulatedTime += price.MaximumTime > totalTimeSpent ? totalTimeSpent : (price.MaximumTime - accumulatedTime) * 1;
            accumulatedCharge += price.Amount;
        }

        // Handle hourly rates
        if (hourlyRatePrices is not null && totalTimeSpent > accumulatedTime) {
            var test = Math.Ceiling(totalTimeSpent - accumulatedTime);
            accumulatedCharge += test * hourlyRatePrices.Amount;
        }

        // Handle daily rates
        if (dayRatePrices is not null && totalDaysSpent >= 1)
            return Math.Ceiling(totalDaysSpent) * dayRatePrices.Amount;

        return accumulatedCharge;
    }
}