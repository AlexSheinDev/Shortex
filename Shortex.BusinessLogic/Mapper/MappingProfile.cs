using AutoMapper;
using Shortex.Common.Models;
using Shortex.Common.Models.DTO;

namespace Shortex.BusinessLogic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShortenedUrl, ShortenedUrlDTO>().ReverseMap();
        }
    }
}
