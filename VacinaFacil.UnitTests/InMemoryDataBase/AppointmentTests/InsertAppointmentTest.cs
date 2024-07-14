using System;
using System.Linq;
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
    public class InsertAppointmentTest : BaseUnitTest
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
        public void InsertAppoinment_Success(ScheduledEnum scheduled)
        {
            var appointment = new InsertAppointmentModel
            {
                IdPatient = 1,
                AppointmentDate = DateTime.Now.AddDays(2),
                AppointmentTime = new TimeSpan(11, 0, 0),
                Scheduled = scheduled
            };

            async Task action() => await _business.InsertAppointment(appointment);

            Assert.DoesNotThrowAsync(action);
        }

        [TestCase(ScheduledEnum.Agendado)]
        [TestCase(ScheduledEnum.Realizado)]
        [TestCase(ScheduledEnum.NaoRealizado)]
        public void InsertAppoinment_Existing_Appointment(ScheduledEnum scheduled)
        {
            var newAppointment = new InsertAppointmentModel
            {
                IdPatient = 1,
                AppointmentDate = DateTime.Now.Date,
                AppointmentTime = new TimeSpan(11, 0, 0),
                Scheduled = scheduled
            };

            AddAppointment(1, DateTime.Now.Date, new TimeSpan(11, 0, 0), scheduled);

            _context.SaveChanges();

            async Task action() => await _business.InsertAppointment(newAppointment);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(BusinessMessages.ExistingAppointment));
        }

        [TestCase(ScheduledEnum.Agendado)]
        [TestCase(ScheduledEnum.Realizado)]
        [TestCase(ScheduledEnum.NaoRealizado)]
        public void InsertAppoinment_2_Existing_Appointments_Hour(ScheduledEnum scheduled)
        {
            var newAppointment = new InsertAppointmentModel
            {
                IdPatient = 3,
                AppointmentDate = DateTime.Now.Date,
                AppointmentTime = new TimeSpan(11, 0, 0),
                Scheduled = scheduled
            };

            AddAppointment(1, DateTime.Now.Date, new TimeSpan(11, 0, 0), scheduled);
            AddAppointment(2, DateTime.Now.Date, new TimeSpan(11, 0, 0), scheduled);

            _context.SaveChanges();

            async Task action() => await _business.InsertAppointment(newAppointment);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(BusinessMessages.AppointmentNotAvailable));
        }

        [TestCase(ScheduledEnum.Agendado)]
        [TestCase(ScheduledEnum.Realizado)]
        [TestCase(ScheduledEnum.NaoRealizado)]
        public void InsertAppoinment_20_Existing_Appointments_Day(ScheduledEnum scheduled)
        {
            var newAppointment = new InsertAppointmentModel
            {
                IdPatient = 21,
                AppointmentDate = DateTime.Now.Date,
                AppointmentTime = new TimeSpan(11, 0, 0),
                Scheduled = scheduled
            };

            for (int i = 1; i <= 20; i++)
            {
                AddAppointment(i, DateTime.Now.Date, new TimeSpan(i, 0, 0), scheduled);
            }

            _context.SaveChanges();

            async Task action() => await _business.InsertAppointment(newAppointment);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(BusinessMessages.AppointmentNotAvailable));
        }
    }
}
