using System;
using System.Collections.Generic;
using System.Text;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.PublicationYear)
                .IsRequired();

            builder.Property(b => b.TotalCopies)
                .IsRequired();

            builder.Property(b => b.AvailableCopies)
                .IsRequired();

            builder.Property(b => b.Price)
                .IsRequired()
                .HasPrecision(18, 2);


            // Index
            builder.HasIndex(b => b.ISBN)
                .IsUnique();

        }
    }
}
