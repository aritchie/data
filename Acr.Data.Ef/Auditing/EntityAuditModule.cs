﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Acr.Data.Ef.Auditing.Mapping;
using Acr.Data.Ef.Modules;


namespace Acr.Data.Ef.Auditing {

    public class EntityAuditModule : DbContextModule {

        protected virtual string ObjectToString(DbContext context, object obj) {
            if (obj == null)
                return String.Empty;

            var bytes = obj as byte[];
            if (bytes != null)
                return Convert.ToBase64String(bytes);

            return obj.ToString();
        }


        protected virtual bool ShouldAudit(DbEntityEntry entry) {
            return !(entry.Entity is EntityAudit) && !(entry.Entity is EntityAuditProperty);
        }


        protected virtual string GetEntityType(object entity) {
            var type = entity.GetType();
            if (type.Namespace == "System.Data.Entity.DynamicProxies")
                type = type.BaseType;

            return type.FullName;
        }


        protected virtual string GetEntityId(DbContext context, DbEntityEntry entry) {
            var state = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            var id = state.EntityKey.EntityKeyValues.FirstOrDefault()?.Value?.ToString() ?? String.Empty;
            return id;
        }


        protected virtual EntityAudit CreateAudit(DbContext context, DbEntityEntry entry, EntityAuditAction action) {

            var audit = new EntityAudit {
                Action = action,
                EntityId = this.GetEntityId(context, entry),
                EntityType = entry.Entity.GetType().FullName,
                DateCreatedUtc = DateTime.UtcNow,
                Properties = new List<EntityAuditProperty>()
            };
            return audit;
        }


        protected virtual void OnAuditStoreInsert(EntityAudit audit, DbContext context, DbEntityEntry entry) {
            auditContext.Audits.Add(audit);
        }


        protected virtual void OnAuditStoreUpdate(EntityAudit audit, DbContext context, DbEntityEntry entry) {
            auditContext.Audits.Add(audit);
        }


        protected virtual void OnAuditStoreDelete(EntityAudit audit, DbContext context, DbEntityEntry entry) {
            auditContext.Audits.Add(audit);
        }


        #region Module Events

        private EntityAuditDbContext auditContext;
        private EntityAudit currentInsert;


        public override void OnModelCreating(DbModelBuilder modelBuilder) {
            // add these to the current context for reading, since EntityAuditDbContext will likely be internal
            modelBuilder.Configurations.Add(new EntityAuditTypeConfiguration());
            modelBuilder.Configurations.Add(new EntityAuditPropertyTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }


        public override void OnPreSaveChanges(DbContext context) {
            this.auditContext = new EntityAuditDbContext(context.Database.Connection);
            base.OnPreSaveChanges(context);
        }


        public override void OnPostSaveChanges(DbContext context) {
            this.auditContext.SaveChanges();
            this.auditContext.Dispose();
            this.auditContext = null;
            base.OnPostSaveChanges(context);
        }


        public override void OnPreInsert(DbContext context, DbEntityEntry entry) {
            if (!this.ShouldAudit(entry))
                return;

            this.currentInsert = this.CreateAudit(context, entry, EntityAuditAction.Insert);
            entry
                .CurrentValues
                .PropertyNames
                .Select(entry.Property)
                .ToList()
                .ForEach(x =>
                    this.currentInsert.Properties.Add(new EntityAuditProperty {
                        PropertyName = x.Name,
                        OldValue = String.Empty,
                        NewValue = this.ObjectToString(context, x.CurrentValue)
                    })
                );

            base.OnPreInsert(context, entry);
        }


        public override void OnPostInsert(DbContext context, DbEntityEntry entry) {
            if (!this.ShouldAudit(entry))
                return;

            this.currentInsert.EntityId = this.GetEntityId(context, entry);
            this.OnAuditStoreInsert(this.currentInsert, context, entry);
            base.OnPostInsert(context, entry);
        }


        public override void OnPreUpdate(DbContext context, DbEntityEntry entry) {
            if (!this.ShouldAudit(entry))
                return;

            var audit = this.CreateAudit(context, entry, EntityAuditAction.Update);
            entry
                .OriginalValues
                .PropertyNames
                .Select(entry.Property)
                .Where(x => x.IsModified)
                .ToList()
                .ForEach(x =>
                    audit.Properties.Add(new EntityAuditProperty {
                        PropertyName = x.Name,
                        OldValue = this.ObjectToString(context, x.OriginalValue),
                        NewValue = this.ObjectToString(context, x.CurrentValue)
                    })
                );

            this.OnAuditStoreUpdate(audit, context, entry);
            base.OnPreUpdate(context, entry);
        }


        public override void OnPreDelete(DbContext context, DbEntityEntry entry) {
            if (!this.ShouldAudit(entry))
                return;

            var audit = this.CreateAudit(context, entry, EntityAuditAction.Delete);
            entry
                .OriginalValues
                .PropertyNames
                .Select(entry.Property)
                .ToList()
                .ForEach(x =>
                    audit.Properties.Add(new EntityAuditProperty {
                        PropertyName = x.Name,
                        OldValue = this.ObjectToString(context, x.OriginalValue),
                        NewValue = String.Empty
                    })
                );

            this.OnAuditStoreDelete(audit, context, entry);
            base.OnPreDelete(context, entry);
        }

        #endregion
    }
}
