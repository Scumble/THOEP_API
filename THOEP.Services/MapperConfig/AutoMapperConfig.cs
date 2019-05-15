using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;
using THOEP.DAL.ViewModel;
using THOEP.Services.DTO;

namespace THOEP.Services.MapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<Disease, DiseaseDto>();
            CreateMap<HealthInfo, HealthInfoDto>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<HealthInfoViewModel, HealthInfoViewModelDto>();
            CreateMap<PatientCoordinates, PatientCoordiantesDto>();
        }
    }
}
