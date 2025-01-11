using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Model.User.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Configuration.UserConfigs
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a=>a.AddressId);
            builder.Property(a => a.AddressId)
                .IsRequired();
            builder.Property(a => a.UserId)
                .IsRequired();
            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.State)
           .IsRequired()
           .HasMaxLength(50);
            builder.Property(a => a.TotalAddress)
           .IsRequired()
           .HasMaxLength(2000);
        }
    }
}
