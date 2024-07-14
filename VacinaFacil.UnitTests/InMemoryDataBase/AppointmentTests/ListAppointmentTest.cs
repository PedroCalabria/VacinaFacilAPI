using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.Enum;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;

namespace VacinaFacil.UnitTests.InMemoryDataBase.AppointmentTests
{
    public class ListAppointmentTest : BaseUnitTest
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

        [Test]
        public void ListAppoinments_Success()
        {
            AddAppointment(1, DateTime.Now.Date, new TimeSpan(11, 0, 0), ScheduledEnum.Realizado);
            AddAppointment(2, DateTime.Now.Date, new TimeSpan(11, 0, 0), ScheduledEnum.NaoRealizado);
            AddAppointment(3, DateTime.Now.Date.AddDays(1), new TimeSpan(12, 0, 0), ScheduledEnum.Agendado);

            async Task action() => await _business.ListAppointments();

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void ListAppoinmentsByDate_Success()
        {
            AddAppointment(1, DateTime.Now.Date, new TimeSpan(11, 0, 0), ScheduledEnum.Realizado);
            AddAppointment(2, DateTime.Now.Date, new TimeSpan(11, 0, 0), ScheduledEnum.NaoRealizado);
            AddAppointment(3, DateTime.Now.Date.AddDays(1), new TimeSpan(12, 0, 0), ScheduledEnum.Agendado);

            async Task action() => await _business.ListAppointmentsByDate(DateTime.Now.Date);

            Assert.DoesNotThrowAsync(action);
        }
    }
}
