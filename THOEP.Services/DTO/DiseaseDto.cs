using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.Services.DTO
{
    public class DiseaseDto
    {
        public int Id { get; set; }
        public string DiseaseCode { get; set; }
        public string DiseaseName { get; set; }
        public float DangerLevel { get; set; }
        public string Recommendation { get; set; }
        public virtual ICollection<HealthInfoDto> HealthInfos { get; set; }
        public DiseaseDto()
        {
            HealthInfos = new List<HealthInfoDto>();
        }
    }
}
