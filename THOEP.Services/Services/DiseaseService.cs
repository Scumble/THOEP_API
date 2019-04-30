using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Interfaces;
using THOEP.DAL.Models;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;

namespace THOEP.Services.Services
{
    public class DiseaseService: IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IMapper _mapper;
        public DiseaseService(IDiseaseRepository diseaseRepository, IMapper mapper)
        {
            _diseaseRepository = diseaseRepository;
            _mapper = mapper;
        }

        public List<DiseaseDto> GetDiseases(int healthInfoId)
        {
            return _mapper.Map<IEnumerable<Disease>, List<DiseaseDto>>(_diseaseRepository.GetDiseases(healthInfoId));
        }

        public DiseaseDto GetDiseaseById(int diseaseId)
        {
            return _mapper.Map<Disease, DiseaseDto>(_diseaseRepository.GetDiseaseById(diseaseId));
        }

        public void AddDisease(DiseaseDto disease)
        {
            _diseaseRepository.AddDisease(_mapper.Map<DiseaseDto, Disease>(disease));
        }

        public void EditDisease(DiseaseDto disease)
        {
            _diseaseRepository.EditDisease(_mapper.Map<DiseaseDto, Disease>(disease));
        }

        public DiseaseDto DeleteDisease(int diseaseId)
        {
            return _mapper.Map<Disease, DiseaseDto>(_diseaseRepository.DeleteDisease(diseaseId));
        }
    }
}
