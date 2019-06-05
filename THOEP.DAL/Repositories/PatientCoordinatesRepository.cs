using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOEP.DAL.DbContext;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;

namespace THOEP.DAL.Repositories
{
    public class PatientCoordinatesRepository: IPatientCoordinatesRepository
    {
        private readonly Context _context;
        public PatientCoordinatesRepository(Context context)
        {
            _context = context;
        }

        public PatientCoordinates GetPatientCoordinates(int patientId)
        {
            return _context.PatientCoordinates.LastOrDefault(x => x.PatientId == patientId);
        }


        public void AddPatientCoordinates(PatientCoordinates coordinates)
        {
            if (coordinates.Id == 0)
            {
                _context.PatientCoordinates.Add(coordinates);
            }
            else
            {
                var dbEntry = _context.PatientCoordinates.Find(coordinates.Id);
                if (dbEntry != null)
                {
                    dbEntry.PatientId = coordinates.PatientId;
                    dbEntry.Longtitude = coordinates.Longtitude;
                    dbEntry.Latitude = coordinates.Latitude;
               
                }
            }
            _context.SaveChanges();
        }
    }
}
