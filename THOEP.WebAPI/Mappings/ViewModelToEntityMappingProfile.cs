using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Models;
using THOEP.WebAPI.ViewModels;

namespace THOEP.Services.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
