using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Configuration.UserConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(200); 

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.FirstName)
               .HasMaxLength(200);

            builder.Property(u => u.LastName)
               .HasMaxLength(200);

            builder.Property(u => u.Email)
               .HasMaxLength(400);

            builder.Property(u => u.Mobile)
             .HasMaxLength(11)
             .IsRequired();

        }
    }
}
