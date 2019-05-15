using AutoMapper;
using HL7.Dotnetcore;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;
using THOEP.DAL.ViewModel;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;

namespace THOEP.Services.Services
{
    public class HealthInfoService: IHealthInfoService
    {
        private readonly IHealthInfoRepository _healthInfoRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public HealthInfoService(IHealthInfoRepository healthInfoRepository, IPatientRepository patientRepository, IMapper mapper)
        {
            _healthInfoRepository = healthInfoRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public List<HealthInfoViewModelDto> GetHealthInfo(int patientId)
        {
            return _mapper.Map<IEnumerable<HealthInfoViewModel>, List<HealthInfoViewModelDto>>(_healthInfoRepository.GetHealthInfo(patientId));
        }

        public List<string> GetHealthInfoEncoded(int patientId)
        {
            var healthInfos = _mapper.Map<IEnumerable<HealthInfoViewModel>, List<HealthInfoViewModelDto>>(_healthInfoRepository.GetHealthInfo(patientId));
            List<string> result = new List<string>();
            for (var i = 0; i < healthInfos.Count; i++)
            {
                Segment OBX = new Segment("OBX", new HL7Encoding());
                OBX.AddNewField(healthInfos[i].Id.ToString(), 1);
                OBX.AddNewField(healthInfos[i].PatientId.ToString(), 2);
                OBX.AddNewField(healthInfos[i].DiseaseCode, 3);
                OBX.AddNewField(healthInfos[i].HeartRate.ToString(), 4);
                OBX.AddNewField("bpm^Heart Rate",5);
                OBX.AddNewField(healthInfos[i].BloodPressure.ToString(), 6);
                OBX.AddNewField("^Blood Pressure", 7);
                OBX.AddNewField(healthInfos[i].Temperature.ToString(), 8);
                OBX.AddNewField("^Temperature", 9);
                OBX.AddNewField(healthInfos[i].Weight.ToString(), 10);
                OBX.AddNewField("kg^Kilogram", 11);
                Message message = new Message();
                message.AddNewSegment(OBX);
                result.Add(message.SerializeMessage(false));
            }
            return result;
        }

        public float GetAverageHeartRate(int patientId)
        {
            return _healthInfoRepository.GetAverageHeartRate(patientId);
        }

        public float GetAverageBloodPressure(int patientId)
        {
            return _healthInfoRepository.GetAverageBloodPressure(patientId);
        }

        public float GetAverageTemperature(int patientId)
        {
            return _healthInfoRepository.GetAverageTemperature(patientId);
        }

        public float GetAverageWeight(int patientId)
        {
            return _healthInfoRepository.GetAverageWeight(patientId);
        }


        public HealthInfoViewModelDto GetHealthInfoById(int healthInfoId)
        {
            return _mapper.Map<HealthInfoViewModel, HealthInfoViewModelDto>(_healthInfoRepository.GetHealthInfoById(healthInfoId));
        }

        public string GetHealthInfoByIdEncoded(int healthInfoId)
        {
            var healthInfo = _mapper.Map<HealthInfoViewModel, HealthInfoViewModelDto>(_healthInfoRepository.GetHealthInfoById(healthInfoId));
            Segment OBX = new Segment("OBX", new HL7Encoding());
            OBX.AddNewField(healthInfo.Id.ToString(), 1);
            OBX.AddNewField(healthInfo.PatientId.ToString(), 2);
            OBX.AddNewField(healthInfo.DiseaseCode, 3);
            OBX.AddNewField(healthInfo.HeartRate.ToString(), 4);
            OBX.AddNewField("bpm^Heart Rate", 5);
            OBX.AddNewField(healthInfo.BloodPressure.ToString(), 6);
            OBX.AddNewField("^Blood Pressure", 7);
            OBX.AddNewField(healthInfo.Temperature.ToString(), 8);
            OBX.AddNewField("^Temperature", 9);
            OBX.AddNewField(healthInfo.Weight.ToString(), 10);
            OBX.AddNewField("kg^Kilogram", 11);
            Message message = new Message();
            message.AddSegmentMSH("THOEP", "StJohn", "CATH", "THOEP", Guid.NewGuid().ToString(), "ADT^001", "MSGID", "P", "2.5");
            message.AddNewSegment(OBX);
            var result = message.SerializeMessage(false);
            return result;
        }

        public void AddHealthInfo(HealthInfoViewModelDto healthInfo)
        {
            _healthInfoRepository.AddHealthInfo(_mapper.Map<HealthInfoViewModelDto, HealthInfoViewModel>(healthInfo));
        }

        public void EditHealthInfo(HealthInfoViewModelDto healthInfo)
        {
            _healthInfoRepository.EditHealthInfo(_mapper.Map<HealthInfoViewModelDto, HealthInfoViewModel>(healthInfo));
        }

        public HealthInfoDto DeleteHealthInfo(int healthInfoId)
        {
            return _mapper.Map<HealthInfo, HealthInfoDto>(_healthInfoRepository.DeleteHealthInfo(healthInfoId));
        }

        public List<string> CheckHealthInfo(int patientId, int healthInfoId)
        {
            var status = new List<string>();
            var healthInfos = _mapper.Map<HealthInfoViewModel, HealthInfoViewModelDto>(_healthInfoRepository.GetHealthInfoById(healthInfoId));
            var patient = _mapper.Map<Patient, PatientDto>(_patientRepository.GetPatientById(patientId));
 
                if(healthInfos.HeartRate>100)
                {
                    status.Add("Ur patient have a quite high heart rate.Please calm down");
                }
                if(healthInfos.HeartRate>130)
                {
                    status.Add("Ur patient  have an abnormal high heart rate.Please contact the doctor");
                }
                if(healthInfos.BloodPressure > 142 && patient.Gender=="Male")
                {
                    status.Add("Ur patient  have a quite high blood pressure (male).");
                }
                if (healthInfos.BloodPressure > 159 && patient.Gender == "Female")
                {
                    status.Add("Ur patient have a quite high blood pressure (female).");
                }
                if(healthInfos.Temperature > 37)
                {
                    status.Add("Ur patient have a quite high temperature.");
                }
                else
            {
                status.Add("Your patient in a perfect state");
            }
            
            return status;
        }
    }
}

