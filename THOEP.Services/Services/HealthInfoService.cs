using AutoMapper;
using HL7.Dotnetcore;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;
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

        public List<HealthInfoDto> GetHealthInfo(int patientId)
        {
            return _mapper.Map<IEnumerable<HealthInfo>, List<HealthInfoDto>>(_healthInfoRepository.GetHealthInfo(patientId));
        }

        public List<string> GetHealthInfoEncoded(int patientId)
        {
            var healthInfos = _mapper.Map<IEnumerable<HealthInfo>, List<HealthInfoDto>>(_healthInfoRepository.GetHealthInfo(patientId));
            List<string> result = new List<string>();
            for (var i = 0; i < healthInfos.Count; i++)
            {
                Segment OBX = new Segment("OBX", new HL7Encoding());
                OBX.AddNewField(healthInfos[i].Id.ToString(), 1);
                OBX.AddNewField(healthInfos[i].PatientId.ToString(), 2);
                OBX.AddNewField(healthInfos[i].DiseaseId.ToString(), 3);
                OBX.AddNewField(healthInfos[i].HeartRate.ToString(), 4);
                OBX.AddNewField("bpm^Heart Rate",5);
                OBX.AddNewField(healthInfos[i].BloodPressure.ToString(), 6);
                OBX.AddNewField("^Blood Pressure", 7);
                OBX.AddNewField(healthInfos[i].Temperature.ToString(), 8);
                OBX.AddNewField("^Temperature", 9);
                OBX.AddNewField(healthInfos[i].Weight.ToString(), 10);
                OBX.AddNewField("kg^Kilogram", 11);
                OBX.AddNewField(healthInfos[i].ActivityPoints.ToString(), 12);
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


        public HealthInfoDto GetHealthInfoById(int healthInfoId)
        {
            return _mapper.Map<HealthInfo, HealthInfoDto>(_healthInfoRepository.GetHealthInfoById(healthInfoId));
        }

        public string GetHealthInfoByIdEncoded(int healthInfoId)
        {
            var healthInfo = _mapper.Map<HealthInfo, HealthInfoDto>(_healthInfoRepository.GetHealthInfoById(healthInfoId));
            Segment OBX = new Segment("OBX", new HL7Encoding());
            OBX.AddNewField(healthInfo.Id.ToString(), 1);
            OBX.AddNewField(healthInfo.PatientId.ToString(), 2);
            OBX.AddNewField(healthInfo.DiseaseId.ToString(), 3);
            OBX.AddNewField(healthInfo.HeartRate.ToString(), 4);
            OBX.AddNewField("bpm^Heart Rate", 5);
            OBX.AddNewField(healthInfo.BloodPressure.ToString(), 6);
            OBX.AddNewField("^Blood Pressure", 7);
            OBX.AddNewField(healthInfo.Temperature.ToString(), 8);
            OBX.AddNewField("^Temperature", 9);
            OBX.AddNewField(healthInfo.Weight.ToString(), 10);
            OBX.AddNewField("kg^Kilogram", 11);
            OBX.AddNewField(healthInfo.ActivityPoints.ToString(), 12);
            Message message = new Message();
            message.AddSegmentMSH("THOEP", "StJohn", "CATH", "THOEP", Guid.NewGuid().ToString(), "ADT^001", "MSGID", "P", "2.5");
            message.AddNewSegment(OBX);
            var result = message.SerializeMessage(false);
            return result;
        }

        public void AddHealthInfo(HealthInfoDto healthInfo)
        {
            _healthInfoRepository.AddHealthInfo(_mapper.Map<HealthInfoDto, HealthInfo>(healthInfo));
        }

        public void EditHealthInfo(HealthInfoDto healthInfo)
        {
            _healthInfoRepository.EditHealthInfo(_mapper.Map<HealthInfoDto, HealthInfo>(healthInfo));
        }

        public HealthInfoDto DeleteHealthInfo(int healthInfoId)
        {
            return _mapper.Map<HealthInfo, HealthInfoDto>(_healthInfoRepository.DeleteHealthInfo(healthInfoId));
        }

        public List<string> CheckHealthInfo(int patientId)
        {
            var status = new List<string>();
            var healthInfos = _mapper.Map<IEnumerable<HealthInfo>, List<HealthInfoDto>>(_healthInfoRepository.GetHealthInfo(patientId));
            var patient = _mapper.Map<Patient, PatientDto>(_patientRepository.GetPatientById(patientId));
            for (var i=0;i< healthInfos.Count; i++)
            {
                if(healthInfos[i].HeartRate>100)
                {
                    status.Add("Ur patient have a quite high heart rate.Please calm down");
                }
                if(healthInfos[i].HeartRate>130)
                {
                    status.Add("Ur patient  have an abnormal high heart rate.Please contact the doctor");
                }
                if(healthInfos[i].BloodPressure > 142 && patient.Gender=="Male")
                {
                    status.Add("Ur patient  have a quite high blood pressure (male).");
                }
                if (healthInfos[i].BloodPressure > 159 && patient.Gender == "Female")
                {
                    status.Add("Ur patient have a quite high blood pressure (female).");
                }
                if(healthInfos[i].Temperature > 37)
                {
                    status.Add("Ur patient have a quite high temperature.");
                }
            }
            return status;
        }
    }
}

