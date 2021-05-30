using AutoMapper;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Domain;
using StandardApi.Mapping.Resolvers;

namespace StandardApi.Mapping.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Message, MessageResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
               .ForMember(dest => dest.Tags, opt => opt.MapFrom<TagsResolver>());
        }
    }
}
