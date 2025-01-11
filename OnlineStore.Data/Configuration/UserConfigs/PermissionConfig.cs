using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Model.User.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Configuration.UserConfigs
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p=>p.PermissionId);
            builder.Property(p=>p.PermissionTitle)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(p => p.PermissionName)
              .IsRequired()
              .HasMaxLength(150);
        }
    }
}
