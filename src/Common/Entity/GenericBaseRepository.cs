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

public class GenericBaseRepository<T, TContext> : IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    where TContext : DbContext {
    private readonly TContext _context;
    protected GenericBaseRepository(TContext context) => _context = context;
    
    public virtual IQueryable<T> GetAll(CancellationToken cancellationToken = default) =>
    _context.Set<T>().AsQueryable();

    public virtual async Task<IEnumerable<T>> GetAll(IPageFilter filter, CancellationToken cancellationToken = default) {
        // var search = filter.Search.Split(" ").ToList().Select(x => x.ToLower());
        return await _context.Set<T>()
               .Skip(filter.Page * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync(cancellationToken: cancellationToken);
    }
    
    public IQueryable<T> GetQueryable(IPageFilter filter, CancellationToken cancellationToken = default) {
        return _context.Set<T>()
               .Skip(filter.Page * filter.PageSize)
               .Take(filter.PageSize)
               .AsQueryable();
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull =>
    await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression) =>
    _context.Set<T>().Where(expression);

    public virtual IQueryable<T> GetQueryable(CancellationToken cancellationToken = default) =>
    _context.Set<T>();
    
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default) {
        _context.Set<T>().Add(entity);
        await this.SaveChangesAsync(cancellationToken);

        return entity;
    }
    
    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
        IEnumerable<T> addRangeAsync = entities.ToList();
        _context.Set<T>().AddRange(addRangeAsync);
        await this.SaveChangesAsync(cancellationToken);

        return addRangeAsync;
    }
    
    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default) {
        _context.Entry(entity).State = EntityState.Modified;
        await this.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
        foreach (var entity in entities) {
            _context.Entry(entity).State = EntityState.Modified;
        }
        await this.SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task ArchiveAsync(T entity, CancellationToken cancellationToken = default) {
        _context.Set<T>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task ArchiveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
        _context.Set<T>().RemoveRange(entities);
        await SaveChangesAsync(cancellationToken);
    }
    
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
    await _context.SaveChangesAsync(cancellationToken);
}