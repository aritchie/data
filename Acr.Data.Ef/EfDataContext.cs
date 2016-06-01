using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;


namespace Acr.Data.Ef
{

    public class EfDataContext : IDataContext
    {
        readonly DbContext data;


        public EfDataContext(DbContext data)
        {
            this.data = data;
        }


        public void SaveChanges()
        {
            this.data.SaveChanges();
        }


        public Task SaveChangesAsync(CancellationToken? cancelToken)
        {
            return this.data.SaveChangesAsync(cancelToken ?? CancellationToken.None);
        }


        public T Add<T>(T obj) where T : class
        {
            return this.data.Set<T>().Add(obj);
        }


        public IQueryable<T> Query<T>() where T : class
        {
            return this.data.Set<T>();
        }


        public T Get<T>(object id) where T : class
        {
            return this.data.Set<T>().Find(id);
        }


        public void Remove<T>(T entity) where T : class
        {
            this.data.Set<T>().Remove(entity);
        }


        public Task<T> GetAsync<T>(object id, CancellationToken? cancelToken) where T : class
        {
            return this.data.Set<T>().FindAsync(id, cancelToken ?? CancellationToken.None);
        }


        public async Task<IEnumerable<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken? cancelToken)
        {
            return await query.ToListAsync(cancelToken ?? CancellationToken.None);
        }


        public async Task<IEnumerable<T>> ToListAsync<T>(Expression<Func<T, bool>> expression, CancellationToken? cancelToken) where T : class
        {
            return await this.data
                .Set<T>()
                .Where(expression)
                .ToListAsync(cancelToken ?? CancellationToken.None);
        }


        public Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, CancellationToken? cancelToken)
        {
            return query.SingleOrDefaultAsync(cancelToken ?? CancellationToken.None);
        }


        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken? cancelToken) where T : class
        {
            return this.data
                .Set<T>()
                .SingleOrDefaultAsync(expression, cancelToken ?? CancellationToken.None);
        }


        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken? cancelToken)
        {
            return query.FirstOrDefaultAsync(cancelToken ?? CancellationToken.None);
        }


        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken? cancelToken) where T : class
        {
            return this.data
                .Set<T>()
                .FirstOrDefaultAsync(expression, cancelToken ?? CancellationToken.None);
        }


        public Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken? cancelToken)
        {
            return query.CountAsync(cancelToken ?? CancellationToken.None);
        }


        public Task<int> CountAsync<T>(Expression<Func<T, bool>> expression, CancellationToken? cancelToken) where T : class
        {
            return this.data
                .Set<T>()
                .CountAsync(expression, cancelToken ?? CancellationToken.None);
        }


        public Task<bool> AnyAsync<T>(IQueryable<T> query, CancellationToken? cancelToken)
        {
            return query.AnyAsync(cancelToken ?? CancellationToken.None);
        }


        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression, CancellationToken? cancelToken) where T : class
        {
            return this.data
                .Set<T>()
                .AnyAsync(expression, cancelToken ?? CancellationToken.None);
        }
    }
}
