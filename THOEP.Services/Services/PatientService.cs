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
    public class PatientService: IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public List<PatientDto> GetPatients()
        {
            return _mapper.Map<IEnumerable<Patient>,List<PatientDto>>(_patientRepository.GetPatients());
        }

        public List<string> GetPatientsEncoded()
        {
            var patients = _mapper.Map<IEnumerable<Patient>, List<PatientDto>>(_patientRepository.GetPatients());
            var result = new List<string>();
            for (var i = 0; i < patients.Count; i++)
            {
                Segment PID = new Segment("PID", new HL7Encoding());
                PID.AddNewField(patients[i].Id.ToString(), 1);
                PID.AddNewField(patients[i].FirstName + "^" + patients[i].LastName, 2);
                PID.AddNewField(patients[i].Gender, 3);
                PID.AddNewField(patients[i].Age, 4);
                PID.AddNewField(patients[i].Address, 5);
                PID.AddNewField(patients[i].BirthDate.Date.ToString(), 6);
                Message message = new Message();
                message.AddSegmentMSH("THOEP", "StJohn", "CATH", "THOEP", Guid.NewGuid().ToString(), "ADT^001", "MSGID", "P", "2.5");
                message.AddNewSegment(PID);
                result.Add(message.SerializeMessage(false));
            }
            return result;
        }

        public PatientDto GetPatientById(int patientId)
        {
            return _mapper.Map<Patient,PatientDto>(_patientRepository.GetPatientById(patientId));
        }

        public string GetPatientByIdEncoded(int patientId)
        {
            var patient = _mapper.Map<Patient, PatientDto>(_patientRepository.GetPatientById(patientId));
            Segment PID = new Segment("PID", new HL7Encoding());
            PID.AddNewField(patient.Id.ToString(), 1);
            PID.AddNewField(patient.FirstName + "^" + patient.LastName, 2);
            PID.AddNewField(patient.Gender, 3);
            PID.AddNewField(patient.Age, 4);
            PID.AddNewField(patient.Address, 5);
            PID.AddNewField(patient.BirthDate.Date.ToString(), 6);
            Message message = new Message();
            message.AddSegmentMSH("THOEP", "StJohn", "CATH", "THOEP", Guid.NewGuid().ToString(), "ADT^001", "MSGID", "P", "2.5");
            message.AddNewSegment(PID);
            var result = message.SerializeMessage(false);
            return result;
        }

        public void AddPatient(PatientDto patient)
        {
            _patientRepository.AddPatient(_mapper.Map<PatientDto,Patient>(patient));
        }

        public void EditPatient(PatientDto patient)
        {
            _patientRepository.EditPatient(_mapper.Map<PatientDto, Patient>(patient));
        }

        public PatientDto DeletePatient(int patientId)
        {
            return _mapper.Map < Patient,PatientDto > (_patientRepository.DeletePatient(patientId));
        }
    }
}
