using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Models;

namespace Storage.Configuration
{
    public class ChiNhanhConfiguration : IEntityTypeConfiguration<ChiNhanh>
    {
        public void Configure(EntityTypeBuilder<ChiNhanh> builder)
        {
            builder
            .HasKey(c => c.Id);

            builder
                .HasOne(c => c.ViTri)
                .WithMany(v => v.ChiNhanhs)
                .HasForeignKey(c => c.IdViTri)
                .IsRequired(false)  // Make the relationship optional
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Cascade based on your requirements

            builder
                .HasMany(c => c.Bookings)
                .WithOne(b => b.ChiNhanh)
                .HasForeignKey(b => b.IdChiNhanh)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Restrict based on your requirements

            builder
                .HasMany(c => c.Barbers)
                .WithOne(b => b.ChiNhanh)
                .HasForeignKey(b => b.IdChiNhanhWork)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
