//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Realms;


//namespace Acr.Data.Realm
//{
//    public class RealmDataContext : IDataContext
//    {
//        readonly Realms.Realm instance;
//        Transaction trans = null;


//        public RealmDataContext(string dbPath = null)
//        {
//            this.instance = Realms.Realm.GetInstance(dbPath);

//            //this.instance.BeginWrite()
//        }


//        public T Add<T>(T obj) where T : class
//        {
//            return null;
//        }


//        public T Get<T>(object id) where T : class
//        {
//            return null;
//        }


//        public void Remove<T>(T entity) where T : class
//        {
//        }


//        public void SaveChanges()
//        {
//            this.trans.Commit();
//        }


//        public Task SaveChangesAsync()
//        {
//            this.SaveChanges();
//            return Task.FromResult<object>(null);
//        }


//        public IQueryable<T> Query<T>() where T : class
//        {
//            //return this.instance.All<T>();
//            return null;
//        }


//        public Task<T> GetAsync<T>(object id) where T : class
//        {
//            return Task.FromResult<T>(null);
//        }


//        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<T>> ToListAsync<T>(Expression<Func<T, bool>> expression) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> CountAsync<T>(IQueryable<T> query)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> AnyAsync<T>(IQueryable<T> query)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<T>> ToListAsync<T>(IQueryable<T> query)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
