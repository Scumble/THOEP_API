using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.DAL.Interfaces
{
    public interface IPatientCoordinatesRepository
    {
        PatientCoordinates GetPatientCoordinates(int patientId);
    }
}
