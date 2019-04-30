using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THOEP.DAL.Models
{
    public class HealthInfo
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int DiseaseId { get; set; }
        public virtual Disease Disease { get; set; }
        public float HeartRate { get; set; }
        public float BloodPressure { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public float ActivityPoints { get; set; }
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
        public bool isFall { get; set; }
    }
}
