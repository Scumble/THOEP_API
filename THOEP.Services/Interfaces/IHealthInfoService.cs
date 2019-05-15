using System;
using System.Collections.Generic;
using System.Text;
using THOEP.Services.DTO;

namespace THOEP.Services.Interfaces
{
    public interface IHealthInfoService
    {
        List<HealthInfoViewModelDto> GetHealthInfo(int patientId);
        List<string> GetHealthInfoEncoded(int patientId);
        void AddHealthInfo(HealthInfoViewModelDto healthInfoDtp);
        void EditHealthInfo(HealthInfoViewModelDto healthInfoDto);
        HealthInfoDto DeleteHealthInfo(int healthInfoId);
        HealthInfoViewModelDto GetHealthInfoById(int healthInfoId);
        string GetHealthInfoByIdEncoded(int healthInfoId);
        float GetAverageHeartRate(int patientId);
        float GetAverageBloodPressure(int patientId);
        float GetAverageTemperature(int patientId);
        float GetAverageWeight(int patientId);
        List<string> CheckHealthInfo(int patientId, int healthInfoId);

    }
}
