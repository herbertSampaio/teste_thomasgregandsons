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
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clients", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Logotipo).IsRequired().HasMaxLength(150);
            builder.Property(c => c.CreateDate);
            builder.Property(c => c.UpdateDate);

            builder.HasMany(c => c.Users).WithOne(u => u.Cliente).HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Logradouros).WithOne(d => d.Cliente).HasForeignKey(d => d.ClienteId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
