// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Linq.Expressions;
using ParkingSpace.Common.Interfaces;

namespace ParkingSpace.Common.Entity;

public interface IReadRepository<TEntity> where TEntity : class, IAggregateRoot {
    IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAll(IPageFilter filter, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetQueryable(IPageFilter filter, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
}