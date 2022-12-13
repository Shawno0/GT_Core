using GT_Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Domain.Entities
{
    public class Comment : ArchivableEntity<int>
    {
        public string? Message { get; set; }
        public Ticket Ticket { get; set; }
    }
}