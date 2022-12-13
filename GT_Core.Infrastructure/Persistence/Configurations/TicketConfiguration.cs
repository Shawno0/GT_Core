using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT_Core.Domain.Entities;
using GT_Core.Infrastructure.Identity;

namespace GT_Core.Infrastructure.Persistence.Configurations
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne<ApplicationUser>()
                .WithMany(e => e.DeveloperTickets)
                .HasForeignKey(e => e.Developer)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne<ApplicationUser>()
                .WithMany(e => e.TesterTickets)
                .HasForeignKey(e => e.Consultant)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}