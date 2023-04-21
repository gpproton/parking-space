// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ParkingSpace.Common.Converter;

public class CollectionValueComparer<T> : ValueComparer<ICollection<T>> {
    public CollectionValueComparer() : base((c1, c2) => c1!.SequenceEqual(c2!),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())), c => (ICollection<T>)c.ToHashSet()) {
    }
}