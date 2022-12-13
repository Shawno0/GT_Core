using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT_Core.Infrastructure.Identity;

namespace GT_Core.Infrastructure.Persistence.Configurations
{
    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable(name: "Roles");

            //builder
            //    .HasKey(r => r.Id);

            //builder
            //    .HasMany(r => r.RoleClaims)
            //    .WithOne(c => c.Role)
            //    .IsRequired();
        }
    }
}