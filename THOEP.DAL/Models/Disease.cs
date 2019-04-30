using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.DAL.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string DiseaseCode { get; set; }
        public string DiseaseName { get; set; }
        public float DangerLevel { get; set; }
        public string Reccomendation { get; set; }
        public virtual ICollection<HealthInfo> HealthInfos { get; set; }
        public Disease()
        {
            HealthInfos = new List<HealthInfo>();
        }
    }
}
