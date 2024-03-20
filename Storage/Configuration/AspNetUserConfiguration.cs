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
    public class AspNetUsersConfiguration : IEntityTypeConfiguration<AspNetUsers>
    {
        public void Configure(EntityTypeBuilder<AspNetUsers> builder)
        {
            builder
             .ToTable("AspNetUsers");

            builder
                .HasOne(u => u.Customer)
                .WithOne(c => c.AspNetUsers)
                .HasForeignKey<Customer>(c => c.IdUserCustomer)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.Cascade based on your requirements

            builder
                .HasOne(u => u.Barber)
                .WithOne(b => b.AspNetUsers)
                .HasForeignKey<Barber>(b => b.IdUserBarber)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
