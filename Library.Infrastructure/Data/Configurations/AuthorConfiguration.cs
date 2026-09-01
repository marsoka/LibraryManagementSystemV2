using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // Primary Key
            builder.HasKey(a => a.Id);

            // Id - Identity
            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            // FullName
            builder.Property(a => a.FullName)
                .IsRequired()
                .HasMaxLength(150);

            // Biography
            builder.Property(a => a.Biography)
                .IsRequired();

            // DateOfBirth
            builder.Property(a => a.DateOfBirth)
                .IsRequired(false);

            // Nationality
            builder.Property(a => a.Nationality)
                .IsRequired()
                .HasMaxLength(100);

            // Relationship: Author -> Books
            builder.HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


