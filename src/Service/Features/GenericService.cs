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
using ParkingSpace.Common.Entity;
using ParkingSpace.Common.Interfaces;
using ParkingSpace.Common.Response;
using ParkingSpace.Filters;

namespace ParkingSpace.Features;

public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class, IAggregateRoot {
    private readonly IRepository<TEntity> _repository;

    protected GenericService(IRepository<TEntity> repository) {
        _repository = repository;
    }

    public virtual async Task<PagedResponse<IEnumerable<TEntity>>> GetAllAsync(IPageFilter? filter) {
        var checkFilter = filter ?? new PageFilter();
        var count = await _repository.GetQueryable().CountAsync();
        var result = await _repository.GetQueryable(checkFilter).ToListAsync();
        // var search = checkFilter.Search.Split(" ").ToList().Select(x => x.ToLower());

        return new PagedResponse<IEnumerable<TEntity>>(result, checkFilter.Page, checkFilter.PageSize, count);
    }

    public virtual async Task<Response<TEntity?>> GetByIdAsync<TId>(TId id) where TId : notnull {
        var result = await _repository.GetByIdAsync(id);
        return new Response<TEntity?>(result);
    }

    public virtual async Task<Response<TEntity>> AddAsync(TEntity entity) {
        var result = await _repository.AddAsync(entity);
        return new Response<TEntity>(result);
    }
    public async Task<Response<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities) {
        var result = await _repository.AddRangeAsync(entities);
        return new Response<IEnumerable<TEntity>>(result);
    }

    public virtual async Task<Response<TEntity?>> UpdateAsync(TEntity entity) {
        await _repository.UpdateAsync(entity);
        return new Response<TEntity?>(entity);
    }
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities) {
        await _repository.UpdateRangeAsync(entities);
    }

    public virtual async Task<Response<TEntity?>> ArchiveAsync<TId>(TId id) where TId : notnull {
        var value = await _repository.GetByIdAsync(id);
        var success = value is not null;
        if (value is not null) await _repository.ArchiveAsync(value);

        return new Response<TEntity?>(value, "", success);
    }
    public async Task ArchiveRangeAsync(IEnumerable<TEntity> entities) {
        await _repository.ArchiveRangeAsync(entities);
    }
    public async Task ClearAsync() {
        await _repository.ClearAsync();
    }
}