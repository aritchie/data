//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Microsoft.Data.Entity;


//namespace Acr.Data.Ef
//{

//    public class EfDataContext : IDataContext
//    {
//        readonly DbContext data;


//        public EfDataContext(DbContext data)
//        {
//            this.data = data;
//        }


//        public void SaveChanges()
//        {
//            this.data.SaveChanges();
//        }


//        public Task SaveChangesAsync()
//        {
//            return this.data.SaveChangesAsync();
//        }


//        public T Add<T>(T obj) where T : class
//        {
//            return this.data.Set<T>().Add(obj).Entity;
//        }


//        public IQueryable<T> Query<T>() where T : class
//        {
//            return this.data.Set<T>();
//        }


//        public T Get<T>(object id) where T : class
//        {
//            var model = this.data.Model
//                .GetEntityTypes()
//                .First(x => x.ClrType == typeof(T));
//            return this.data.Set<T>().FirstOrDefault();
//        }

//        public void Remove<T>(T entity) where T : class
//        {
//            this.data.Set<T>().Remove(entity);
//        }


//        public Task<T> GetAsync<T>(object id) where T : class
//        {
//            return this.data.Set<T>().FindAsync(id);
//        }


//        public async Task<IEnumerable<T>> ToListAsync<T>(IQueryable<T> query)
//        {
//            return await query.ToListAsync();
//        }


//        public async Task<IEnumerable<T>> ToListAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            return await this.data
//                .Set<T>()
//                .Where(expression)
//                .ToListAsync();
//        }


//        public Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query)
//        {
//            return query.SingleOrDefaultAsync();
//        }


//        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            return this.data
//                .Set<T>()
//                .SingleOrDefaultAsync(expression);
//        }


//        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query)
//        {
//            return query.FirstOrDefaultAsync();
//        }


//        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            return this.data
//                .Set<T>()
//                .FirstOrDefaultAsync(expression);
//        }


//        public Task<int> CountAsync<T>(IQueryable<T> query)
//        {
//            return query.CountAsync();
//        }


//        public Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            return this.data
//                .Set<T>()
//                .CountAsync(expression);
//        }


//        public Task<bool> AnyAsync<T>(IQueryable<T> query)
//        {
//            return query.AnyAsync();
//        }


//        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            return this.data
//                .Set<T>()
//                .AnyAsync(expression);
//        }
//    }
//}
