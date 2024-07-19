using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Enum;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository;

namespace VacinaFacil.UnitTests
{
    public class BaseUnitTest
    {
        private readonly IServiceCollection ServiceCollection = new ServiceCollection();
        protected Context _context;
        
        protected void Register<I, T>() where I : class where T : class, I
          => ServiceCollection.AddSingleton<I, T>();

        protected I GetService<I>() where I : class
          => ServiceCollection.BuildServiceProvider().GetService<I>();

        protected void RegisterObject<Tp, T>(Tp type, T objeto) where Tp : Type where T : class
           => ServiceCollection.AddSingleton(type, objeto);

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            ConfigureInMemoryDataBase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            _context.Dispose();
        }

        private void ConfigureInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<Context>()
                              .UseInMemoryDatabase("InMemoryDatabase")
            .Options;

            _context = new Context(options);

            if (_context.Database.IsInMemory())
                _context.Database.EnsureDeleted();
        }

        protected void AddAppointment(int idPatient, DateTime date, TimeSpan time, ScheduledEnum scheduled)
        {
            var appointment = new Appointment
            {
                IdPatient = idPatient,
                AppointmentDate = date,
                AppointmentTime = time,
                Scheduled = scheduled,
                CreationDate = DateTime.Now
            };

            _context.Add(appointment);
        }

        protected static Patient CreatePatient(InsertPatientModel newPatient)
        {
            var patient = new Patient
            {
                Name = newPatient.Name,
                BirthDate = newPatient.BirthDate,
                Email = newPatient.Email,
                CreationDate = DateTime.Now
            };

            using var hmac = new HMACSHA512();
            patient.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPatient.Password));
            patient.PasswordSalt = hmac.Key;

            return patient;
        }
    }
}