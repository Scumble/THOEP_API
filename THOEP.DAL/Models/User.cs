using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; } 
        public string Location { get; set; }
        public string Locale { get; set; }
    }
}
