using Library.Domain.Entities.auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .UseIdentityColumn(1, 1);


            builder.Property(x => x.RevokedReason)
                .HasConversion<string>();


            builder.HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId);

            builder.Property(r => r.TokenHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasIndex(r => r.TokenHash)
                .IsUnique();

        }
    }
}