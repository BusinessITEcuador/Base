using hexagonal.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.infrastructure.data.configs
{
    public class CuentaConfig : IEntityTypeConfiguration<CuentaEntity>
    {
        public void Configure(EntityTypeBuilder<CuentaEntity> builder)
        {
            builder.ToTable("Cuenta");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.LastModified)
                .HasColumnName("LastModified")
                .HasDefaultValueSql("('1900-01-01')");

            builder.Property(x => x.LastModifiedBy)
                .HasColumnName("LastModifiedBy")
                .HasDefaultValueSql("('00000000-0000-0000-0000-000000000000')");
        }
    }
}