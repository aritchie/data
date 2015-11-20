using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;


namespace Acr.Data.Ef {

    public static class DbContextExtensions {

        public static DbPropertyEntry TryGetProperty(this DbEntityEntry entry, string propertyName) {
            try {
                return entry.Property(propertyName);
            }
            catch {
                return null;
            }
        }


        public static bool HasChanges(this DbContext context) {
            return context
                .ChangeTracker
                .Entries()
                .Any(x => x.State != EntityState.Unchanged);
        }


        public static bool HasChanges(this DbContext context, object entity) {
            return (context.Entry(entity).State != EntityState.Unchanged);
        }


        public static bool IsPropertyDirty<T, TProperty>(this DbContext context, T obj, Expression<Func<T, TProperty>> expression) where T : class {
            return context.Entry(obj).Property(expression).IsModified;
        }


        public static bool IsPropertyDirty(this DbContext context, object obj, string property) {
            return context.Entry(obj).Property(property).IsModified;
        }


        public static bool IsLoaded<T, TProperty>(this DbContext context, T obj, Expression<Func<T, TProperty>> expression)
                where T : class
                where TProperty : class {
            return context.Entry(obj).Reference(expression).IsLoaded;
        }


        public static bool IsLoaded(this DbContext context, object obj, string property) {
            return context.Entry(obj).Reference(property).IsLoaded;
        }


        public static TProperty GetPreviousValue<TEntity, TProperty>(this DbContext context, TEntity obj, Expression<Func<TEntity, TProperty>> expression) where TEntity : class {
            return context.Entry(obj).Property(expression).OriginalValue;
        }


        public static object GetPreviousValue(this DbContext context, object obj, string property) {
            return context.Entry(obj).Property(property).OriginalValue;
        }


        public static T GetPreviousValue<T>(this DbContext context, object obj, string property) {
            return (T)context.GetPreviousValue(obj, property);
        }


        public static T Lazy<T>(this T context, bool enable) where T : DbContext {
            context.Configuration.LazyLoadingEnabled = enable;
            context.Configuration.ProxyCreationEnabled = enable;
            return context;
        }
    }
}
