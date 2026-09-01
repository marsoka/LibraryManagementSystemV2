using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Data.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Name
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            // Address
            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(300);

            // Phone
            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(30);

            // Relationship: Publisher -> Books
            builder.HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
