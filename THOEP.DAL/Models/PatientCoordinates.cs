using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.DAL.Models
{
    public class PatientCoordinates
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public float Longtitude { get; set; }
        public float Latitude { get; set; }
    }
}
