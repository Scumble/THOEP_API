using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOEP.DAL.DbContext;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;
using THOEP.DAL.ViewModel;

namespace THOEP.DAL.Repositories
{
    public class HealthInfoRepository : IHealthInfoRepository
    {
        private readonly Context _context;
        public HealthInfoRepository(Context context)
        {
            _context = context;
        }

        public List<HealthInfoViewModel> GetHealthInfo(int patientId)
        {
            var results = (from p in _context.HealthInfos
                           join ps in _context.Diseases on p.DiseaseId equals ps.Id
                           where p.PatientId == patientId
                           select new HealthInfoViewModel()
                           {
                               Id = p.Id,
                               PatientId = p.PatientId,
                               DiseaseCode = ps.DiseaseCode,
                               HeartRate = p.HeartRate,
                               BloodPressure = p.BloodPressure,
                               Temperature = p.Temperature,
                               Weight = p.Weight,
                               Time = p.Time
                           }).ToList();

            return results;

        }

        public HealthInfoViewModel GetHealthInfoById(int healthInfoId)
        {
            var results = (from p in _context.HealthInfos
                           join ps in _context.Diseases on p.DiseaseId equals ps.Id
                           where p.Id == healthInfoId
                           select new HealthInfoViewModel()
                           {
                               Id = p.Id,
                               PatientId = p.PatientId,
                               DiseaseCode = ps.DiseaseCode,
                               HeartRate = p.HeartRate,
                               BloodPressure = p.BloodPressure,
                               Temperature = p.Temperature,
                               Weight = p.Weight,
                               Time = p.Time
                           }).ToList();
            return results[0];
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

        public void AddHealthInfo(HealthInfoViewModel healthInfo)
        {
            var diseaseCode = _context.Diseases.Where(x => x.DiseaseCode == healthInfo.DiseaseCode).FirstOrDefault();
            var healthInfos = new HealthInfo();
            if (healthInfo.Id == 0)
            {
                healthInfos.PatientId = healthInfo.PatientId;
                healthInfos.DiseaseId = diseaseCode.Id;
                healthInfos.HeartRate = healthInfo.HeartRate;
                healthInfos.BloodPressure = healthInfo.BloodPressure;
                healthInfos.Temperature = healthInfo.Temperature;
                healthInfos.Weight = healthInfo.Weight;
                healthInfos.Time = healthInfo.Time;
                _context.HealthInfos.Add(healthInfos);
            }
            else
            {
                var dbEntry = _context.HealthInfos.Find(healthInfo.Id);
                if (dbEntry != null)
                {
                    dbEntry.PatientId = healthInfo.PatientId;
                    dbEntry.DiseaseId = diseaseCode.Id;
                    dbEntry.HeartRate = healthInfo.HeartRate;
                    dbEntry.BloodPressure = healthInfo.BloodPressure;
                    dbEntry.Temperature = healthInfo.Temperature;
                    dbEntry.Weight = healthInfo.Weight;
                    dbEntry.Time = healthInfo.Time;
                    _context.HealthInfos.Add(dbEntry);
                }
            }
            _context.SaveChanges();
        }

        public void EditHealthInfo(HealthInfoViewModel healthInfo)
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
