using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;
using THOEP.Services.DTO;

namespace THOEP.Services.Interfaces
{
    public interface IPatientService
    {
        List<PatientDto> GetPatients();
        List<string> GetPatientsEncoded();
        void AddPatient(PatientDto patient);
        void EditPatient(PatientDto patient);
        PatientDto DeletePatient(int patientId);
        PatientDto GetPatientById(int patientId);
        string GetPatientByIdEncoded(int patientId);
    }
}
