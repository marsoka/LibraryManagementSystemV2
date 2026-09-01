using System;
using System.Collections.Generic;
using System.Text;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.HasKey(m => m.Id);



            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(254);


            builder.Property(m => m.Phone)
                .IsRequired()
                .HasMaxLength(30);


            builder.Property(m => m.Address)
                .IsRequired()
                .HasMaxLength(300);


            builder.Property(m => m.RegistrationDate)
                .IsRequired();


            // =========================
            // Indexes
            // =========================

            builder.HasIndex(m => m.Email)
                .IsUnique();


            // =========================
            // Relationship
            // =========================

            builder.HasMany(m => m.Borrowings)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
