using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.DAL.Interfaces
{
    public interface IPatientRepository
    {
        List<Patient> GetPatients();
        void AddPatient(Patient patient);
        void EditPatient(Patient patient);
        Patient DeletePatient(int patientId);
        Patient GetPatientById(int patientId);
    }
}
