// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ParkingSpace.Common.Interfaces;
using ParkingSpace.Common.Response;

namespace ParkingSpace.Features;

public interface IGenericService<TEntity> where TEntity : class, IAggregateRoot {
    Task<PagedResponse<IEnumerable<TEntity>>> GetAllAsync(IPageFilter? filter);
    Task<Response<TEntity?>> GetByIdAsync<TId>(TId id) where TId : notnull;
    Task<Response<TEntity>> AddAsync(TEntity entity);
    Task<Response<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities);
    Task<Response<TEntity?>> UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    Task<Response<TEntity?>> ArchiveAsync<TId>(TId entity) where TId : notnull;
    Task ArchiveRangeAsync(IEnumerable<TEntity> entities);
}