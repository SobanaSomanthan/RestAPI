using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Villa, VillaDto>(); // <Source,Destiny>
            //Custom mapping we have to use for
            CreateMap<VillaDto, Villa>(); // Reverse mapping

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
