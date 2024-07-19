using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacinaFacil.Entity.Entities;

namespace VacinaFacil.Repository.Map
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("tb_agendamento");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id_agendamento")
                .IsRequired();

            builder.Property(e => e.IdPatient)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(e => e.AppointmentDate)
                .HasColumnName("dat_agendamento")
                .IsRequired();

            builder.Property(e => e.AppointmentTime)
                .HasColumnName("hor_agendamento")
                .IsRequired();

            builder.Property(e => e.Scheduled)
                .HasColumnName("dsc_status")
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .HasColumnName("dat_criacao")
                .IsRequired();
        }
    }
}
