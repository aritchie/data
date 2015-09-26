using System;
using System.Data.Entity.ModelConfiguration;


namespace Acr.Data.Ef.Auditing.Mapping {

    public class EntityAuditTypeConfiguration : EntityTypeConfiguration<EntityAudit> {

        public EntityAuditTypeConfiguration() {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("AuditID");
            this.Property(x => x.Action).IsRequired();
            this.Property(x => x.DateCreatedUtc).IsRequired();

            this.Property(x => x.EntityId).HasMaxLength(50).IsRequired();
            this.Property(x => x.EntityType).HasMaxLength(500).IsRequired();

            this.HasMany(x => x.Properties)
                .WithRequired(x => x.EntityAudit)
                .HasForeignKey(x => x.EntityAuditId);
        }
    }
}
