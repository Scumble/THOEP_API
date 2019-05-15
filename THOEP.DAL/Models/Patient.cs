using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THOEP.DAL.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<HealthInfo> HealthInfos { get; set; }
        public virtual ICollection<PatientCoordinates> PatientCoordinates { get; set; }
        public Patient()
        {
            HealthInfos = new List<HealthInfo>();
            PatientCoordinates = new List<PatientCoordinates>();
        }
    }
}
