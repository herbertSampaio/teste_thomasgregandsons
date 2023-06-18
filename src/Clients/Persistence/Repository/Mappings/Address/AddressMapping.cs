using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Addres>
    {
        public void Configure(EntityTypeBuilder<Addres> builder)
        {
            builder.ToTable("Address", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(c => c.Logradouro).IsRequired().HasMaxLength(100);
            builder.Property(c => c.CreateDate);
            builder.Property(c => c.UpdateDate);

            builder.HasOne(c => c.Cliente).WithMany().HasForeignKey(x => x.ClienteId);
        }
    }
}
