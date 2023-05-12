using GT_Core.Domain.Common;
using GT_Core.Domain.Entities;
using GT_Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Status> Statuses { get; }
        DbSet<Severity> Severities { get; }
        DbSet<Ticket> Tickets { get; }
        DbSet<Comment> Comments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}