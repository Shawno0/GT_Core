using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT_Core.Domain.Entities;
using GT_Core.Domain.Common;
using GT_Core.Domain.Interfaces;

namespace GT_Core.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public virtual List<Ticket> DeveloperTickets { get; set; }
        public virtual List<Ticket> TesterTickets { get; set; }
    }
}