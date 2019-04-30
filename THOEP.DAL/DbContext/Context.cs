using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.DAL.DbContext
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options): base(options)
        {

        }
        public new DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HealthInfo> HealthInfos { get; set; }
        public DbSet<Disease> Diseases { get; set; }
    }
}
