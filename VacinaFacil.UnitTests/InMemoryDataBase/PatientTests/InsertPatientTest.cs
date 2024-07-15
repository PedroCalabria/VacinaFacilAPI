using VacinaFacil.Business.Businesses;
using VacinaFacil.Business.Interface.IBusinesses;
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
            var patient = new InsertPatientModel
            {
                BirthDate = DateTime.Now.AddYears(-21),
                Name = "Test",
                Email = "test@Email",
                Password = "password",
            };

            async Task action() => await _business.InsertPatient(patient);

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void InsertPatient_Existing_Patient()
        {
            var newPatient = new InsertPatientModel
            {
                BirthDate = DateTime.Now.Date.AddYears(-21),
                Name = "Test",
                Email = "Test@email",
                Password = "password",
            };

            var patient = CreatePatient(newPatient);

            var existingPatient = new InsertPatientModel
            {
                BirthDate = DateTime.Now.Date.AddYears(-21),
                Name = "Test",
                Email = "Test@email",
                Password = "password",
            };

            _context.Add(patient);
            _context.SaveChanges();

            async Task action() => await _business.InsertPatient(newPatient);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(string.Format(BusinessMessages.ExistingRecord, existingPatient.Email)));
        }
    }
}
