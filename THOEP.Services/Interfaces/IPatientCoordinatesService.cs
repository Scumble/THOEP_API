using System;
using System.Collections.Generic;
using System.Text;
using THOEP.Services.DTO;

namespace THOEP.Services.Interfaces
{
    public interface IPatientCoordinatesService
    {
        PatientCoordiantesDto GetPatientCoordinates(int patientId);
        void AddPatientCoordinates(PatientCoordiantesDto coordiantesDto);
    }
}
