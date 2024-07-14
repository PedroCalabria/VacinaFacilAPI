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
    public class DeletePatientTest : BaseUnitTest
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
        public void DeletePatient_Success()
        {
            var patient = new Patient
            {
                Id = 1,
                BirthDate = DateTime.Now.AddYears(-21),
                Name = "Test",
                CriationDate = DateTime.Now,
            };

            _context.Add(patient);

            _context.SaveChanges();

            async Task action() => await _business.DeletePatient(1);

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void DeletePatient_Non_Existing_Patient()
        {
            async Task action() => await _business.DeletePatient(1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.That(exception.Message, Is.EqualTo(BusinessMessages.RecordNotFound));
        }
    }
}
