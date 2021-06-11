using System;
using System.Collections.Generic;
using PatientPortal.Api;
using PatientPortal.Api.Models;
using PatientPortal.Api.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PatientPortal.Api.Services
{
    public interface IPatientService
    {
        IEnumerable<PatientModel> GetByIds(Guid[] patientIds);
        Task<PatientModel> Upsert(PatientModel patient);
        Task<IEnumerable<Guid>> DeleteByIds(Guid[] patientIds);
    }

    public class PatientService : IPatientService
    {
        private readonly PatientPortalDbContext _dbContext;
        private readonly IPatientMapper _patientMapper;

        public PatientService(PatientPortalDbContext dbContext, IPatientMapper patientMapper)
        {
            _dbContext = dbContext;
            _patientMapper = patientMapper;
        }

        public IEnumerable<PatientModel> GetByIds(params Guid[] patientIds)
        {
            var patients = patientIds.Length == 0 ?
                _dbContext.Patients.Where(p => !p.IsDeleted) :
                _dbContext.Patients.Where(p =>
                    patientIds.Contains(p.Id) && !p.IsDeleted);

            return patients.Select(_patientMapper.MapPatientModel);
        }

        public async Task<PatientModel> Upsert(PatientModel patientModel)
        {
            var patient = _dbContext.Patients
                .SingleOrDefault(p => p.Id == patientModel.Id && !p.IsDeleted);

            if (patient == null)
            {
                patient = _patientMapper.MapPatient(patientModel);
                await _dbContext.Patients.AddAsync(patient);
            }
            else
            {
                patient = _patientMapper.MapPatient(patientModel);
            }

            await _dbContext.SaveChangesAsync();

            return _patientMapper.MapPatientModel(patient);
        }

        public async Task<IEnumerable<Guid>> DeleteByIds(params Guid[] patientIds)
        {
            var patients = _dbContext.Patients
                .Where(p => patientIds.Contains(p.Id) && !p.IsDeleted);

            var deletedIds = patients.Select(p => p.Id).ToList();

            foreach (var patient in patients)
                patient.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return deletedIds;
        }
    }
}
