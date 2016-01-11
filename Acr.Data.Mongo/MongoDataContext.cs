using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace Acr.Data.Mongo {

    public class MongoDataContext : IDataContext {
        readonly IMongoDatabase data; // should be static


        public MongoDataContext(IMongoDatabase data) {
            this.data = data;
        }


        public T Add<T>(T obj) where T : class {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync<T>(IQueryable<T> query) {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class {
            return this.data
                .GetCollection<T>(typeof(T).Name)
                .AsQueryable()
                .Where(expression)
                .AnyAsync();
        }

        public Task<int> CountAsync<T>(IQueryable<T> query) {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : class {
            throw new NotImplementedException();
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query) {
            throw new NotImplementedException();
        }

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class {
            throw new NotImplementedException();
        }

        public T Get<T>(object id) where T : class {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(object id) where T : class {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : class {
            throw new NotImplementedException();
        }

        public void Remove<T>(T entity) where T : class {
            throw new NotImplementedException();
        }

        public void SaveChanges() {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync() {
            throw new NotImplementedException();
        }

        public Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query) {
            throw new NotImplementedException();
        }

        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ToListAsync<T>(IQueryable<T> query) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ToListAsync<T>(Expression<Func<T, bool>> expression) where T : class {
            throw new NotImplementedException();
        }
    }
}
