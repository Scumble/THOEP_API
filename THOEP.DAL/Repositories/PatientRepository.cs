using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using THOEP.DAL.DbContext;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;

namespace THOEP.DAL.Repositories
{
    public class PatientRepository: IPatientRepository
    {
        private readonly Context _context;
        private readonly ClaimsPrincipal _caller;
        public PatientRepository(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;
        }

        public List<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientById(int patientId)
        {
            var patient = _context.Patients.Find(patientId);
            return patient;
        }

        public void AddPatient(Patient patient)
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            patient.UserId = userId.Value;
            if (patient.Id == 0)
            {
                _context.Patients.Add(patient);
            }
            else
            {
                var dbEntry = _context.Patients.Find(patient.Id);
                if (dbEntry != null)
                {
                    dbEntry.UserId = userId.Value;
                    dbEntry.FirstName = patient.FirstName;
                    dbEntry.LastName = patient.LastName;
                    dbEntry.Gender = patient.Gender;
                    dbEntry.Age = patient.Age;
                    dbEntry.Address = patient.Address;
                    dbEntry.BirthDate = patient.BirthDate;
                    dbEntry.isActive = true;
                }
            }
            _context.SaveChanges();
        }

        public void EditPatient(Patient patient)
        {
            _context.Entry(patient).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public Patient DeletePatient(int patientId)
        {
            var dbEntry = _context.Patients.Find(patientId);
            if (dbEntry != null)
            {
                _context.Patients.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
