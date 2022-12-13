using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT_Core.Domain.Entities;

namespace GT_Core.Infrastructure.Persistence.Configurations
{
    internal class SeverityConfiguration : IEntityTypeConfiguration<Severity>
    {
        public void Configure(EntityTypeBuilder<Severity> builder)
        {
            builder
                .HasKey(e => e.Id);
        }
    }
}