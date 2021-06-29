using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Mappings.AutoMapper
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<HistoryAddDto, History>(); // Creates a map from Dto to Entity type
            CreateMap<History, HistoryGetDto>(); // Creates a map from Entity to Dto type
            CreateMap<HistoryGetDto, HistoryAddDto>(); // Creates a map from Dto to Dto type
        }
    }
}