using hexagonal.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $safeprojectname$.configs
{
  public class PersonConfig : IEntityTypeConfiguration<PersonEntity>
  {
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
      builder.ToTable("TBL_PERSON");

      builder.HasKey(x => x.Id);

      builder.Property(u => u.Id)
           .ValueGeneratedOnAdd();

      builder.Property(x => x.Name)
        .HasColumnName("NAME")
        .IsRequired();
    }
  }
}
