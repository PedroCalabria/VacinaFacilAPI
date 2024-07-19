using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacinaFacil.UnitTests.InMemoryDataBase
{
    using global::VacinaFacil.Business.Businesses;
    using global::VacinaFacil.Business.Interface.IBusinesses;
    using global::VacinaFacil.Entity.Entities;
    using global::VacinaFacil.Entity.Enum;
    using global::VacinaFacil.Entity.Model;
    using global::VacinaFacil.Repository.Interface.IRepositories;
    using global::VacinaFacil.Repository.Repositories;
    using global::VacinaFacil.Utils.Exceptions;
    using global::VacinaFacil.Utils.Messages;
    using System;

    namespace VacinaFacil.UnitTests.InMemoryDataBase.AppointmentTests
    {
        public class UpdatePatientTest : BaseUnitTest
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
            public void UpdateAppoinment_Success()
            {
                var newPatient = new InsertPatientModel
                {
                    BirthDate = DateTime.Now.Date.AddYears(-21),
                    Name = "Test",
                    Email = "Test@email",
                    Password = "password",
                };

                var patient = CreatePatient(newPatient);

                var updatePatient = new UpdatePatientModel
                {
                    Name = "Test2",
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = "Test@email"
                };

                _context.Add(patient);
                _context.SaveChanges();

                async Task action() => await _business.UpdatePatient(1, updatePatient);

                Assert.DoesNotThrowAsync(action);
            }

            [Test]
            public void UpdatePatient_Non_Existing_Patient()
            {
                var newPatient = new UpdatePatientModel
                {
                    Name = "Test2",
                    BirthDate = DateTime.Now.AddYears(-18),
                    Email = "Test@email",
                };

                async Task action() => await _business.UpdatePatient(1, newPatient);

                var exception = Assert.ThrowsAsync<BusinessException>(action);

                Assert.That(exception.Message, Is.EqualTo(BusinessMessages.RecordNotFound));
            }
        }
    }

}
