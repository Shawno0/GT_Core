using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Domain.Common
{
    public abstract class ArchivableEntity<TKey> : AuditableEntity<TKey>
    {
        public DateTime? Archived { get; set; }

        public string? ArchivedBy { get; set; }
    }
}