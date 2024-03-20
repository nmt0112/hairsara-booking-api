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
    public class DichVuConfiguration : IEntityTypeConfiguration<DichVu>
    {
        public void Configure(EntityTypeBuilder<DichVu> builder)
        {
            builder
            .HasKey(v => v.Id);

            builder
                .HasOne(v => v.DanhMuc)
                .WithMany(d => d.DichVus)
                .HasForeignKey(v => v.IdDanhMuc)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Cascade based on your requirements

            // Explicitly specify the SQL Server column type for the Gia property
            builder
                .Property(v => v.Gia)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
