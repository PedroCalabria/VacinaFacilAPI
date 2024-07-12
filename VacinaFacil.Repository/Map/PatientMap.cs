using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacinaFacil.Entity.Entities;

namespace VacinaFacil.Repository.Map
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("tb_paciente");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("dsc_nome")
                .IsRequired();

            builder.Property(e => e.BirthDate)
                .HasColumnName("dat_nascimento")
                .IsRequired();

            builder.Property(e => e.CriationDate)
                .HasColumnName("dat_criacao")
                .IsRequired();
        }
    }
}
