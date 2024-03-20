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
    public class DanhMucConfiguration : IEntityTypeConfiguration<DanhMuc>
    {
        public void Configure(EntityTypeBuilder<DanhMuc> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .HasMany(d => d.DichVus)
                .WithOne(v => v.DanhMuc)
                .HasForeignKey(v => v.IdDanhMuc)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Cascade based on your requirements
        }
    }
}
