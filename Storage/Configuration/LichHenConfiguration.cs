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
    public class LichHenConfiguration : IEntityTypeConfiguration<LichHen>
    {
        public void Configure(EntityTypeBuilder<LichHen> builder)
        {
            builder
            .HasKey(l => l.Id);

            builder
                .Property(l => l.TrangThaiHoanThanh)
                .IsRequired();
            builder
                .Property(b => b.ThoiGianKetThuc)
                .IsRequired();
            builder
                .HasOne(l => l.Booking)
                .WithOne(b => b.LichHen)
                .HasForeignKey<LichHen>(l => l.IdBooking)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }    
}
