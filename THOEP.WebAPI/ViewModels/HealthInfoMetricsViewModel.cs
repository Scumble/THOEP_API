using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace THOEP.WebAPI.ViewModels
{
    public class HealthInfoMetricsViewModel
    {
        public float AverageHeartRate { get; set; }
        public float AverageBloodPressure { get; set; }
        public float AverageTemperature { get; set; }
        public float AverageWeight { get; set; }
    }
}
