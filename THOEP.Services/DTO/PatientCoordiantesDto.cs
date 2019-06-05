using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.Services.DTO
{
    public class PatientCoordiantesDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual PatientDto Patient { get; set; }
        public float Longtitude { get; set; }
        public float Latitude { get; set; }

        public PatientCoordiantesDto(int patientId,float longtitude, float latitude)
        {
            PatientId = patientId;
            Longtitude = longtitude;
            Latitude = latitude;
        }
        public PatientCoordiantesDto() { }
    }
}
