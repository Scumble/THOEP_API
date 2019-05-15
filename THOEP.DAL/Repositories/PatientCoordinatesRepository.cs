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
    }
}
