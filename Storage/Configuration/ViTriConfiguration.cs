using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Configuration
{
    public class ViTriConfiguration : IEntityTypeConfiguration<ViTri>
    {
        public void Configure(EntityTypeBuilder<ViTri> builder)
        {
            builder
            .HasKey(v => v.Id);

            builder
                .HasMany(v => v.ChiNhanhs)
                .WithOne(c => c.ViTri)
                .HasForeignKey(c => c.IdViTri)
                .IsRequired(false)  // Make the relationship optional
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
