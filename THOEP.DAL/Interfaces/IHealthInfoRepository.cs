using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;

namespace THOEP.DAL.Interfaces
{
    public interface IHealthInfoRepository
    {
        List<HealthInfo> GetHealthInfo(int patientId);
        void AddHealthInfo(HealthInfo healthInfo);
        void EditHealthInfo(HealthInfo healthInfo);
        HealthInfo DeleteHealthInfo(int healthInfoId);
        HealthInfo GetHealthInfoById(int healthInfoId);
        float GetAverageHeartRate(int patientId);
        float GetAverageBloodPressure(int patientId);
        float GetAverageTemperature(int patientId);
        float GetAverageWeight(int patientId);
    }
}
