using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOEP.DAL.DbContext;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;

namespace THOEP.DAL.Repositories
{
    public class HealthInfoRepository : IHealthInfoRepository
    {
        private readonly Context _context;
        public HealthInfoRepository(Context context)
        {
            _context = context;
        }

        public List<HealthInfo> GetHealthInfo(int patientId)
        {
            return _context.HealthInfos.Where(x => x.PatientId == patientId).ToList();
        }

        public HealthInfo GetHealthInfoById(int healthInfoId)
        {
            var healthInfo = _context.HealthInfos.Find(healthInfoId);
            return healthInfo;
        }

        public float GetAverageHeartRate(int patientId)
        {
            var avgHearthRate = _context.HealthInfos.Where(x => x.PatientId == patientId)
                .Select(x => x.HeartRate).Average();

            return avgHearthRate;
        }

        public float GetAverageBloodPressure(int patientId)
        {
            var avgBloodPressure = _context.HealthInfos.Where(x => x.PatientId == patientId)
                .Select(x => x.BloodPressure).Average();

            return avgBloodPressure;
        }

        public float GetAverageWeight(int patientId)
        {
            var avgWeight = _context.HealthInfos.Where(x => x.PatientId == patientId)
                .Select(x => x.Weight).Average();

            return avgWeight;
        }

        public float GetAverageTemperature(int patientId)
        {
            var avgTemperature = _context.HealthInfos.Where(x => x.PatientId == patientId)
                .Select(x => x.Temperature).Average();

            return avgTemperature;
        }

        public void AddHealthInfo(HealthInfo healthInfo)
        {
            if (healthInfo.Id == 0)
            {
                _context.HealthInfos.Add(healthInfo);
            }
            else
            {
                var dbEntry = _context.HealthInfos.Find(healthInfo.Id);
                if (dbEntry != null)
                {
                    dbEntry.PatientId = healthInfo.PatientId;
                    dbEntry.DiseaseId = healthInfo.DiseaseId;
                    dbEntry.HeartRate = healthInfo.HeartRate;
                    dbEntry.BloodPressure = healthInfo.BloodPressure;
                    dbEntry.Temperature = healthInfo.Temperature;
                    dbEntry.Weight = healthInfo.Weight;
                    dbEntry.Time = healthInfo.Time;    
                    dbEntry.ActivityPoints = healthInfo.ActivityPoints;
                    dbEntry.isFall = healthInfo.isFall;
                }
            }
            _context.SaveChanges();
        }

        public void EditHealthInfo(HealthInfo healthInfo)
        {
            _context.Entry(healthInfo).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public HealthInfo DeleteHealthInfo(int healthInfoId)
        {
            var dbEntry = _context.HealthInfos.Find(healthInfoId);
            if (dbEntry != null)
            {
                _context.HealthInfos.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
