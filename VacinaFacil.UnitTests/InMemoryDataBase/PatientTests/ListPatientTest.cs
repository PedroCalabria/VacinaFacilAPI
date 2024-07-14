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
            var patient = new Patient
            {
                Id = 1,
                BirthDate = DateTime.Now.Date.AddYears(-21),
                Name = "Test",
                CriationDate = DateTime.Now,
            };

            var patient2 = new Patient
            {
                Id = 2,
                BirthDate = DateTime.Now.Date.AddYears(-18),
                Name = "Test",
                CriationDate = DateTime.Now,
            };

            _context.Add(patient);
            _context.Add(patient2);
            _context.SaveChanges();

            async Task action() => await _business.ListPatients();

            Assert.DoesNotThrowAsync(action);
        }
    }
}
