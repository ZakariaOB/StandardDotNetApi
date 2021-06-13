using AutoMapper;
using StandardApi.Contracts.Contracts.V1.Requests.Queries;
using StandardApi.Domain;

namespace StandardApi.Mapping.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
