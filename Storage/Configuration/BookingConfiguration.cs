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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
            .HasKey(b => b.Id);

            builder
                .Property(b => b.ThoiGianBatDau)
                .IsRequired();


            builder
                .Property(b => b.TrangThai)
                .IsRequired();

            builder
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.IdCustomer)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Restrict based on your requirements

            builder
                .HasOne(b => b.Barber)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.IdBarber)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Restrict based on your requirements

            builder
                .HasOne(b => b.DichVu)
                .WithMany(d => d.Bookings)
                .HasForeignKey(b => b.IdDichVu)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Restrict based on your requirements

            builder
                .HasOne(b => b.ChiNhanh)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.IdChiNhanh)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Cascade based on your requirements

            builder
                .HasOne(b => b.LichHen)
                .WithOne(l => l.Booking)
                .HasForeignKey<LichHen>(l => l.IdBooking)
                .OnDelete(DeleteBehavior.Cascade); // or DeleteBehavior.Restrict based on your requirements

            builder
                .HasOne(b => b.HuyLichHen)
                .WithOne(h => h.Booking)
                .HasForeignKey<HuyLichHen>(h => h.IdBooking)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
