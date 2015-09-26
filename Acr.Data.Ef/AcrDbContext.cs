using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Acr.Data.Ef.Modules;


namespace Acr.Data.Ef {

    public class AcrDbContext : DbContext {

        public static List<IDbContextModule> Modules { get; } = new List<IDbContextModule>();


        public AcrDbContext() { }
        public AcrDbContext(DbCompiledModel model) : base(model) { }
        public AcrDbContext(string connectionStringOrName) : base(connectionStringOrName) { }
        public AcrDbContext(string connectionStringOrName, DbCompiledModel model) : base(connectionStringOrName, model) {}


        #region Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            Modules.ToList().ForEach(x => x.OnModelCreating(modelBuilder));
            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges() {
            Modules.ForEach(x => x.OnPreSaveChanges(this));
            var entries = this.ChangeTracker.Entries().ToList();
            this.OnSavingChanges(entries);
            var result = base.SaveChanges();
            this.OnSavedChanges(entries);
            Modules.ForEach(x => x.OnPostSaveChanges(this));
            return result;
        }

        #endregion

        #region Virtuals

        List<DbEntityEntry> inserts = new List<DbEntityEntry>();
        List<DbEntityEntry> updates = new List<DbEntityEntry>();
        List<DbEntityEntry> deletes = new List<DbEntityEntry>();


        protected virtual void OnSavingChanges(IEnumerable<DbEntityEntry> entries) {
            entries.ToList().ForEach(entry => {
                switch (entry.State) {
                    case EntityState.Added:
                        this.inserts.Add(entry);
                        Modules.ForEach(x => x.OnPreInsert(this, entry));
                        break;

                    case EntityState.Deleted:
                        this.deletes.Add(entry);
                        Modules.ForEach(x => x.OnPreDelete(this, entry));
                        break;

                    case EntityState.Modified:
                        this.updates.Add(entry);
                        Modules.ForEach(x => x.OnPreUpdate(this, entry));
                        break;
                }
            });
        }


        protected virtual void OnSavedChanges(IEnumerable<DbEntityEntry> entries) {
            if (!Modules.Any())
                return;

            Modules.ForEach(x => {
                this.inserts.ForEach(y => x.OnPostInsert(this, y));
                this.updates.ForEach(y => x.OnPostUpdate(this, y));
                this.deletes.ForEach(y => x.OnPostDelete(this, y));
            });
        }

        #endregion
    }
}
