using Infrastructure.Persistance.EFC.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.EFC.Configurations;

internal sealed class MemberEntityConfiguration : IEntityTypeConfiguration<MemberEntity>
{
    public void Configure(EntityTypeBuilder<MemberEntity> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .IsRequired();
    }
}
