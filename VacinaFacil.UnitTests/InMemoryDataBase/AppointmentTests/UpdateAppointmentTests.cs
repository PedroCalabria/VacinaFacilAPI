using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacinaFacil.UnitTests.InMemoryDataBase
{
    using global::VacinaFacil.Business.Businesses;
    using global::VacinaFacil.Business.Interface.IBusinesses;
    using global::VacinaFacil.Entity.Enum;
    using global::VacinaFacil.Entity.Model;
    using global::VacinaFacil.Repository.Interface.IRepositories;
    using global::VacinaFacil.Repository.Repositories;
    using global::VacinaFacil.Utils.Exceptions;
    using global::VacinaFacil.Utils.Messages;
    using System;

    namespace VacinaFacil.UnitTests.InMemoryDataBase.AppointmentTests
    {
        public class UpdateAppointmentTest : BaseUnitTest
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
            public void UpdateAppoinment_Success(ScheduledEnum scheduled)
            {
                var newAppointment = new UpdateAppointmentModel
                {
                    AppointmentDate = DateTime.Now.AddDays(2),
                    AppointmentTime = new TimeSpan(11, 0, 0),
                    Scheduled = scheduled
                };

                AddAppointment(1, DateTime.Now.Date, new TimeSpan(11, 0, 0), scheduled);

                _context.SaveChanges();

                async Task action() => await _business.UpdateAppointment(1, newAppointment);

                Assert.DoesNotThrowAsync(action);
            }

            [TestCase(ScheduledEnum.Agendado)]
            [TestCase(ScheduledEnum.Realizado)]
            [TestCase(ScheduledEnum.NaoRealizado)]
            public void UpdateAppoinment_Non_Existing_Appointment(ScheduledEnum scheduled)
            {
                var newAppointment = new UpdateAppointmentModel
                {
                    AppointmentDate = DateTime.Now.Date,
                    AppointmentTime = new TimeSpan(11, 0, 0),
                    Scheduled = scheduled
                };

                async Task action() => await _business.UpdateAppointment(1, newAppointment);

                var exception = Assert.ThrowsAsync<BusinessException>(action);

                Assert.That(exception.Message, Is.EqualTo(BusinessMessages.RecordNotFound));
            }

            [TestCase(ScheduledEnum.Agendado)]
            [TestCase(ScheduledEnum.Realizado)]
            [TestCase(ScheduledEnum.NaoRealizado)]
            public void UpdateAppoinment_2_Existing_Appointments_Hour(ScheduledEnum scheduled)
            {
                var newAppointment = new UpdateAppointmentModel
                {
                    AppointmentDate = DateTime.Now.Date,
                    AppointmentTime = new TimeSpan(11, 0, 0),
                    Scheduled = scheduled
                };

                AddAppointment(1, DateTime.Now.Date, new TimeSpan(11, 0, 0), scheduled);
                AddAppointment(2, DateTime.Now.Date, new TimeSpan(11, 0, 0), scheduled);
                AddAppointment(3, DateTime.Now.Date, new TimeSpan(12, 0, 0), scheduled);

                _context.SaveChanges();

                async Task action() => await _business.UpdateAppointment(3, newAppointment);

                var exception = Assert.ThrowsAsync<BusinessException>(action);

                Assert.That(exception.Message, Is.EqualTo(BusinessMessages.AppointmentNotAvailable));
            }

            [TestCase(ScheduledEnum.Agendado)]
            [TestCase(ScheduledEnum.Realizado)]
            [TestCase(ScheduledEnum.NaoRealizado)]
            public void Update_Appoinment_20_Existing_Appointments_Day(ScheduledEnum scheduled)
            {
                var newAppointment = new UpdateAppointmentModel
                {
                    AppointmentDate = DateTime.Now.Date,
                    AppointmentTime = new TimeSpan(11, 0, 0),
                    Scheduled = scheduled
                };

                for (int i = 1; i <= 20; i++)
                {
                    AddAppointment(i, DateTime.Now.Date, new TimeSpan(i, 0, 0), scheduled);
                }

                AddAppointment(21, DateTime.Now.Date.AddDays(1), new TimeSpan(11, 0, 0), scheduled);

                _context.SaveChanges();

                async Task action() => await _business.UpdateAppointment(21, newAppointment);

                var exception = Assert.ThrowsAsync<BusinessException>(action);

                Assert.That(exception.Message, Is.EqualTo(BusinessMessages.AppointmentNotAvailable));
            }
        }
    }

}
