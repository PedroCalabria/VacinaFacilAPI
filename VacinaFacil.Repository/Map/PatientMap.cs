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

            builder.Property(e => e.Email)
                .HasColumnName("dsc_email")
                .IsRequired();

            builder.Property(e => e.PasswordHash)
               .HasColumnName("psw_hash")
               .IsRequired();

            builder.Property(e => e.PasswordSalt)
                .HasColumnName("psw_salt")
                .IsRequired();
            
            builder.Property(e => e.CreationDate)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.HasMany(e => e.Appointments)
                .WithOne(p => p.Patient)
                .HasForeignKey(e => e.Id);
        }
    }
}
