using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.DAL.ViewModel
{
    public class HealthInfoViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string DiseaseCode { get; set; }
        public float HeartRate { get; set; }
        public float BloodPressure { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}
