using System;
using System.Collections.Generic;
using System.Text;
using THOEP.Services.DTO;

namespace THOEP.Services.Interfaces
{
    public interface IHealthInfoService
    {
        List<HealthInfoDto> GetHealthInfo(int patientId);
        List<string> GetHealthInfoEncoded(int patientId);
        void AddHealthInfo(HealthInfoDto healthInfoDtp);
        void EditHealthInfo(HealthInfoDto healthInfoDto);
        HealthInfoDto DeleteHealthInfo(int healthInfoId);
        HealthInfoDto GetHealthInfoById(int healthInfoId);
        string GetHealthInfoByIdEncoded(int healthInfoId);
        float GetAverageHeartRate(int patientId);
        float GetAverageBloodPressure(int patientId);
        float GetAverageTemperature(int patientId);
        float GetAverageWeight(int patientId);
        List<string> CheckHealthInfo(int patientId);

    }
}
