// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
//
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.Json.Serialization;

namespace ParkingSpace.Common.Entity {
    public class AuditableEntity<TKey> : CoreEntity<TKey>, IAuditableEntity<TKey> {
        [JsonIgnore]
        public TKey? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        [JsonIgnore]
        public TKey? UpdatedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset? UpdatedAt { get; set; }
        [JsonIgnore]
        public TKey? ArchivedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset? ArchivedAt { get; set; }
    }
}