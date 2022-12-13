using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Domain.Common
{
    public abstract class AuditableEntity<TKey> : Entity<TKey>
    {
        public AuditableEntity()
        {
            Created = DateTime.Now;
            LastModified = DateTime.Now;
        }

        public DateTime? Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}