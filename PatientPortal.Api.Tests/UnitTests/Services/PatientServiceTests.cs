using PatientPortal.Services;
using Xunit;
using PatientPortal.Api.Mappers;
using System;
using PatientPortal.Api.Domain;
using System.Linq;
using System.Collections.Generic;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Tests.Services
{
    public class PatientServiceTests : TestDatabaseFixture
    {
        private readonly PatientPortalDbContext _dbContext;
        private readonly IPatientMapper _patientMapper;
        private readonly PatientService _sut;

        public PatientServiceTests(TestDatabaseContext dbContext)
            : base(dbContext)
        {
            _dbContext = Fixture.CreateDbContext();

            _dbContext.Patients.AddRange(new List<Patient>
            {
                new Patient { FirstName = "fn1", LastName = "ln1" },
                new Patient { FirstName = "fn2", LastName = "ln2" },
                new Patient { FirstName = "fn3", LastName = "ln3" },
                new Patient { FirstName = "fn4", LastName = "ln4" },
                new Patient { FirstName = "fn5", LastName = "ln5" },
                new Patient { FirstName = "fn6", LastName = "ln6", IsDeleted = true }
            });

            _dbContext.SaveChanges();

            _patientMapper = new PatientMapper();

            _sut = new PatientService(_dbContext, _patientMapper);
        }

        [Fact]
        public void GetAllByIds_WithNoIdList_ShouldNotBeEmpty()
        {
            var patients = _sut.GetByIds();
            Assert.NotEmpty(patients);
        }

        [Fact]
        public void GetAllByIds_WithDeletedId_ShouldBeEmpty()
        {
            var deleted = _dbContext.Patients.First(p => p.IsDeleted);
            var patients = _sut.GetByIds(deleted.Id);
            Assert.Empty(patients);
        }

        [Fact]
        public void GetAllByIds_WithId_ShouldReturnPatient()
        {
            var patient = _dbContext.Patients.First(p => !p.IsDeleted);
            var patients = _sut.GetByIds(patient.Id);
            Assert.NotEmpty(patients);
            Assert.Single(patients);
            Assert.Equal(patient.Id, patients.Single().Id.Value);
        }

        [Fact]
        public async void Upsert_WithNewPatient_ShouldInsert()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);

            var sut = new PatientService(dbContext, _patientMapper);

            var patientModel = new PatientModel { FirstName = "fn", LastName = "ln" };

            var newPatient = await sut.Upsert(patientModel);

            Assert.NotNull(newPatient);
            Assert.NotNull(newPatient.Id);

            var patients = sut.GetByIds(newPatient.Id.Value);
            Assert.NotEmpty(patients);
            Assert.Equal(newPatient.Id.Value, patients.First().Id);
        }

        [Fact]
        public async void Upsert_WithExistingPatient_ShouldUpdate()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new PatientService(dbContext, _patientMapper);

            var existingPatient = new Patient { FirstName = "fn", LastName = "ln" };
            dbContext.Patients.Add(existingPatient);
            dbContext.SaveChanges();

            var existingPatientModel = _patientMapper.MapPatientModel(existingPatient);
            existingPatientModel.FirstName = "newFN";
            existingPatientModel.LastName = "newLN";

            var updatedPatientModel = await sut.Upsert(existingPatientModel);

            Assert.NotNull(updatedPatientModel);
            Assert.NotNull(updatedPatientModel.Id);
            Assert.Equal(existingPatientModel.Id, updatedPatientModel.Id);
            Assert.Equal(existingPatientModel.FirstName, updatedPatientModel.FirstName);
            Assert.Equal(existingPatientModel.LastName, updatedPatientModel.LastName);
        }

        [Fact]
        public async void DeleteByIds_WithNoIdList_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new PatientService(dbContext, _patientMapper);

            var patient = new Patient { FirstName = "fn", LastName = "ln" };
            dbContext.Patients.Add(patient);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds();

            Assert.Empty(deletedIds);

            var patientModels = sut.GetByIds(patient.Id);

            Assert.NotEmpty(patientModels);
            Assert.Equal(patient.Id, patientModels.First().Id.Value);
        }

        [Fact]
        public async void DeleteByIds_WithIdList_ShouldNotBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new PatientService(dbContext, _patientMapper);

            var patient1 = new Patient { FirstName = "fn", LastName = "ln" };
            var patient2 = new Patient { FirstName = "fn", LastName = "ln" };
            dbContext.Patients.AddRange(patient1, patient2);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds(patient1.Id, patient2.Id);

            Assert.NotEmpty(deletedIds);
            Assert.Contains(patient1.Id, deletedIds);
            Assert.Contains(patient2.Id, deletedIds);

            var patientModels = sut.GetByIds(patient1.Id, patient2.Id);

            Assert.Empty(patientModels);
        }
    }
}
