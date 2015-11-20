using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Acr.Data {

    public interface IDataContext {
        T Add<T>(T obj) where T : class;
        T Get<T>(object id) where T : class;
        void Remove<T>(T entity) where T : class;

        void SaveChanges();
        Task SaveChangesAsync();
        IQueryable<T> Query<T>() where T : class;

        Task<T> GetAsync<T>(object id) where T : class;
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> ToListAsync<T>(Expression<Func<T, bool>> expression) where T : class;

        Task<int> CountAsync<T>(IQueryable<T> query);
        Task<bool> AnyAsync<T>(IQueryable<T> query);
        Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query);
        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);
        Task<IEnumerable<T>> ToListAsync<T>(IQueryable<T> query);
    }
}
