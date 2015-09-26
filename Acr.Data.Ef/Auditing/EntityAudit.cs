using System;
using System.Collections.Generic;


namespace Acr.Data.Ef.Auditing {

    public enum EntityAuditAction {
        Insert = 1,
        Update = 2,
        Delete = 3
    }


    public class EntityAudit {

        public virtual long Id { get; set; }
        public virtual string EntityId { get; set; }
        public virtual string EntityType { get; set; }
        public virtual EntityAuditAction Action { get; set; }
        public virtual DateTime DateCreatedUtc { get; set; }

        public virtual IList<EntityAuditProperty> Properties { get; set; }
    }
}
