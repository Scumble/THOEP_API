using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;
using THOEP.DAL.ViewModel;

namespace THOEP.DAL.Interfaces
{
    public interface IHealthInfoRepository
    {
        List<HealthInfoViewModel> GetHealthInfo(int patientId);
        void AddHealthInfo(HealthInfoViewModel healthInfo);
        void EditHealthInfo(HealthInfoViewModel healthInfo);
        HealthInfo DeleteHealthInfo(int healthInfoId);
        HealthInfoViewModel GetHealthInfoById(int healthInfoId);
        float GetAverageHeartRate(int patientId);
        float GetAverageBloodPressure(int patientId);
        float GetAverageTemperature(int patientId);
        float GetAverageWeight(int patientId);
    }
}
