using System;
using System.Collections.Generic;
using System.Text;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{

    public class BorrowingConfiguration
        : IEntityTypeConfiguration<Borrowing>
    {
        public void Configure(
            EntityTypeBuilder<Borrowing> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BorrowDate)
                .IsRequired();

            builder.Property(b => b.DueDate)
                .IsRequired();

            builder.Property(b => b.ReturnDate)
                .IsRequired(false);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            // Index
            builder.HasIndex(b => b.DueDate);
        }
    }
}
