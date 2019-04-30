using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using THOEP.Services.DTO;

namespace THOEP.WebAPI.ViewModels
{
    public class HealthInfoViewModel
    {
        public string Patient { get; set; }
        public List<string> HealthInfo { get; set; }
    }
}
