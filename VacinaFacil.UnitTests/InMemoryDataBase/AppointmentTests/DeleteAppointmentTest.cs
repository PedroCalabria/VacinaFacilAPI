using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Enum;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;
using VacinaFacil.Utils.Exceptions;
using VacinaFacil.Utils.Messages;

namespace VacinaFacil.UnitTests.InMemoryDataBase.AppointmentTests
{
    public class DeleteAppointmentTest : BaseUnitTest
    {
        private IAppointmentBusiness _business;
        private IAppointmentRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new AppointmentRepository(_context);

            RegisterObject(typeof(IAppointmentRepository), _repository);

            Register<IAppointmentBusiness, AppointmentBusiness>();

            _business = GetService<IAppointmentBusiness>();
        }

        [TestCase(ScheduledEnum.Agendado)]
        [TestCase(ScheduledEnum.Realizado)]
        [TestCase(ScheduledEnum.NaoRealizado)]
        public void DeleteAppoinment_Success(ScheduledEnum scheduled)
        {
            var appointment = new Appointment
            {
                Id = 1,
                IdPatient = 1,
                AppointmentDate = DateTime.Now.Date,
                AppointmentTime = new TimeSpan(10, 0, 0),
                Scheduled = scheduled,
                CriationDate = DateTime.Now
            };

            _context.Add(appointment);

            _context.SaveChanges();

            async Task action() => await _business.DeleteAppointment(1);

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void DeleteAppoinment_Non_Existing_Appointment()
        {
            async Task action() => await _business.DeleteAppointment(1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(BusinessMessages.RecordNotFound));
        }
    }
}
