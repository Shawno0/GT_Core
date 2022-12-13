using GT_Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Domain.Entities
{
    public class Ticket : ArchivableEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; }
        public Status Status { get; set; }
        public string Developer { get; set; }
        public string Consultant { get; set; }
        public List<Comment> Comments { get; set; }
    }
}