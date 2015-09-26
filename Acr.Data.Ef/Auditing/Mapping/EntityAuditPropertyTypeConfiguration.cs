using System;
using System.Data.Entity.ModelConfiguration;


namespace Acr.Data.Ef.Auditing.Mapping {

    public class EntityAuditPropertyTypeConfiguration : EntityTypeConfiguration<EntityAuditProperty> {

        public EntityAuditPropertyTypeConfiguration() {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("AuditId");
            this.Property(x => x.PropertyName).HasMaxLength(255).IsRequired();
            this.Property(x => x.OldValue).HasMaxLength(null).IsRequired();
            this.Property(x => x.NewValue).HasMaxLength(null).IsRequired();
            this.Property(x => x.EntityAuditId).IsRequired();

            this.HasRequired(x => x.EntityAudit)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.EntityAuditId);
        }
    }
}
