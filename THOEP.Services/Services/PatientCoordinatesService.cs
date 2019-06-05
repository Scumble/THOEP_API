using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;
using THOEP.DAL.Repositories;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;

namespace THOEP.Services.Services
{
    public class PatientCoordinatesService: IPatientCoordinatesService
    {
        private readonly IPatientCoordinatesRepository _patientCoordinatesRepository;
        private readonly IMapper _mapper;
        public PatientCoordinatesService(IPatientCoordinatesRepository patientCoordinatesRepository, IMapper mapper)
        {
            _patientCoordinatesRepository = patientCoordinatesRepository;
            _mapper = mapper;
        }

        public PatientCoordiantesDto GetPatientCoordinates(int patientId)
        {
            return _mapper.Map<PatientCoordinates, PatientCoordiantesDto>(_patientCoordinatesRepository.GetPatientCoordinates(patientId));
        }

        public void AddPatientCoordinates(PatientCoordiantesDto coordiantesDto)
        {
            _patientCoordinatesRepository.AddPatientCoordinates(_mapper.Map<PatientCoordiantesDto, PatientCoordinates>(coordiantesDto));
        } 

    }
}
