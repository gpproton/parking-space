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
using Microsoft.EntityFrameworkCore;
using ParkingSpace.Common.Interfaces;

namespace ParkingSpace.Common.Entity;

public class GenericBaseRepository<TEntity, TContext, TId> : IRepository<TEntity>
    where TId : notnull
    where TEntity : class, IAggregateRoot, IHasKey<TId>
    where TContext : DbContext {
    private readonly TContext _context;
    protected GenericBaseRepository(TContext context) => _context = context;
    
    public virtual IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default) =>
    _context.Set<TEntity>().AsQueryable();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IPageFilter filter, CancellationToken cancellationToken = default) {
        return await _context.Set<TEntity>()
               .Take(filter.PageSize)
               .Skip(filter.Page - 1 * filter.PageSize)
               .ToListAsync(cancellationToken: cancellationToken);
    }
    
    public IQueryable<TEntity> GetQueryable(IPageFilter filter, CancellationToken cancellationToken = default) {
        var query = _context.Set<TEntity>().OrderBy(x => x.Id);
        return query
                .Take(filter.PageSize)
                .Skip(filter.Page - 1 * filter.PageSize)
                .AsQueryable();
    }

    public virtual async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull =>
    await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    
    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression) =>
    _context.Set<TEntity>().Where(expression);

    public virtual IQueryable<TEntity> GetQueryable(CancellationToken cancellationToken = default) =>
    _context.Set<TEntity>();
    
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _context.Set<TEntity>().Add(entity);
        await this.SaveChangesAsync(cancellationToken);

        return entity;
    }
    
    public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        IEnumerable<TEntity> addRangeAsync = entities.ToList();
        _context.Set<TEntity>().AddRange(addRangeAsync);
        await this.SaveChangesAsync(cancellationToken);

        return addRangeAsync;
    }
    
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _context.Entry(entity).State = EntityState.Modified;
        await this.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        foreach (var entity in entities) {
            _context.Entry(entity).State = EntityState.Modified;
        }
        await this.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task ArchiveAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _context.Set<TEntity>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task ArchiveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        _context.Set<TEntity>().RemoveRange(entities);
        await SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
    await _context.SaveChangesAsync(cancellationToken);
}