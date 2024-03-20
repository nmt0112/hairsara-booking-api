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
    public class BarberConfiguration : IEntityTypeConfiguration<Barber>
    {
        public void Configure(EntityTypeBuilder<Barber> builder)
        {
            builder
            .HasKey(b => b.Id);

            builder
                .HasOne(b => b.ChiNhanh)
                .WithMany(c => c.Barbers)
                .HasForeignKey(b => b.IdChiNhanhWork)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Cascade based on your requirements

            builder
                .HasOne(b => b.AspNetUsers)
                .WithOne(u => u.Barber)
                .HasForeignKey<Barber>(b => b.IdUserBarber)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
