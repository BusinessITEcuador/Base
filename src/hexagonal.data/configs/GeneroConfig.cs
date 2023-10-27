using hexagonal.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hexagonal.infrastructure.data.configs
{
    public class GeneroConfig : IEntityTypeConfiguration<GeneroEntity>
    {
        public void Configure(EntityTypeBuilder<GeneroEntity> builder)
        {
            builder.ToTable("Genero");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nombre)
                .HasColumnName("Nombre")
                .IsRequired();
        }
    }
}