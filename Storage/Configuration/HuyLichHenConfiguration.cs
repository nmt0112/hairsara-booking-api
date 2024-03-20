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
    public class HuyLichHenConfiguration : IEntityTypeConfiguration<HuyLichHen>
    {
        public void Configure(EntityTypeBuilder<HuyLichHen> builder)
        {
            builder
            .HasKey(h => h.Id);

            builder
                .Property(h => h.LyDoHuyLich)
                .IsRequired();

            builder
                .Property(h => h.ThoiGianHuy)
                .IsRequired();

            builder
                .HasOne(h => h.Booking)
                .WithOne(b => b.HuyLichHen)
                .HasForeignKey<HuyLichHen>(h => h.IdBooking)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Restrict based on your requirements           
        }
    }
}
