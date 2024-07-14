using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Entity.Model;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Repository.Repositories;
using VacinaFacil.Utils.Exceptions;
using VacinaFacil.Utils.Messages;

namespace VacinaFacil.UnitTests.InMemoryDataBase.AppointmentTests
{
    public class InsertPatientTest : BaseUnitTest
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
        public void InsertPatient_Success()
        {
            var patient = new PatientModel
            {
                BirthDate = DateTime.Now.AddYears(-21),
                Name = "Test",
            };

            async Task action() => await _business.InsertPatient(patient);

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void InsertPatient_Existing_Patient()
        {
            var patient = new Patient
            {
                Id = 1,
                BirthDate = DateTime.Now.Date.AddYears(-21),
                Name = "Test",
                CriationDate = DateTime.Now,
            };

            var newPatient = new PatientModel
            {
                BirthDate = DateTime.Now.Date.AddYears(-21),
                Name = "Test",
            };

            _context.Add(patient);
            _context.SaveChanges();

            async Task action() => await _business.InsertPatient(newPatient);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(string.Format(BusinessMessages.ExistingRecord, new { newPatient.Name, newPatient.BirthDate })));
        }
    }
}
