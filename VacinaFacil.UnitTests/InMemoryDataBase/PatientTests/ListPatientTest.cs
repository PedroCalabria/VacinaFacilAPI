using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Enum;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;

namespace VacinaFacil.UnitTests.InMemoryDataBase.AppointmentTests
{
    public class ListPatientTest : BaseUnitTest
    {
        private IPatientBusiness _business;
        private IPatientRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new PatientRepository(_context);

            RegisterObject(typeof(IPatientRepository), _repository);

            Register<IPatientBusiness, PatientBusiness>();

            _business = GetService<IPatientBusiness>();
        }

        [Test]
        public void ListPatients_Success()
        {
            var newPatient = new InsertPatientModel
            {
                BirthDate = DateTime.Now.Date.AddYears(-21),
                Name = "Test",
                Email = "Test@email",
                Password = "password",
            };

            var patient = CreatePatient(newPatient);

            _context.Add(patient);
            _context.SaveChanges();

            async Task action() => await _business.ListPatients();

            Assert.DoesNotThrowAsync(action);
        }
    }
}
