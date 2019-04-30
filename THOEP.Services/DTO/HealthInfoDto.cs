using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.Services.DTO
{
    public class HealthInfoDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual PatientDto Patient { get; set; }
        public string DiseaseId { get; set; }
        public virtual DiseaseDto Disease { get; set; }
        public float HeartRate { get; set; }
        public float BloodPressure { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public DateTime Time { get; set; }
        public float ActivityPoints { get; set; }
        public bool isFall { get; set; }
    }
}
