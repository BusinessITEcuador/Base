using hexagonal.domain.entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.infrastructure.data.configs
{
     public class IdiomaConfig : IEntityTypeConfiguration<IdiomaEntity>
        {
            public void Configure(EntityTypeBuilder<IdiomaEntity> builder)
            {
                builder.ToTable("Idioma");

                builder.HasKey(x => x.Id);

                builder.Property(u => u.Id)
                    .ValueGeneratedOnAdd();

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
