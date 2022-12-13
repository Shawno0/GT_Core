using GT_Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Domain.Entities
{
    public class Severity : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}