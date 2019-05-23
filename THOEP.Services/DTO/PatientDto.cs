using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.Services.DTO
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUserDto AppUser;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<HealthInfoDto> HealthInfos { get; set; }
        public virtual ICollection<PatientCoordiantesDto> PatientCoordinates { get; set; }
        public PatientDto()
        {
            HealthInfos = new List<HealthInfoDto>();
            PatientCoordinates = new List<PatientCoordiantesDto>();
        }
    }
}
