using System;


namespace Acr.Data.Ef.Auditing {

    public class EntityAuditProperty {

        public virtual long Id { get; set; }
        public virtual string PropertyName { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }

        public virtual long EntityAuditId { get; set; }
        public virtual EntityAudit EntityAudit { get; set; }
    }
}
