using System;
using PatientPortal.Api.Domain;
using PatientPortal.Api.Models;

namespace PatientPortal.Api.Mappers
{
    public interface IPatientMapper
    {
        Patient MapPatient(PatientModel patientModel);
        PatientModel MapPatientModel(Patient patient);
    }

    public class PatientMapper : IPatientMapper
    {
        public Patient MapPatient(PatientModel patientModel)
        {
            return new()
            {
                FirstName = patientModel.FirstName,
                LastName = patientModel.LastName,
            };
        }

        public PatientModel MapPatientModel(Patient patient)
        {
            return new()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
            };
        }
    }
}
