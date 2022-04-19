using AutoMapper;
using MDEvents.Services.Specification.SpecificationParameters;
using Project.Tereza.Core;
using Project.Tereza.Requests.Requests;
using Project.Tereza.Requests.Requests.Specifications;

namespace Project.Tereza.Api.MappingConfiguration.Response
{
    public class RequestsProfile : Profile
    {
        public RequestsProfile()
        {
            CreateMap<AddNeedRequest, Need>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<UpdateNeedRequest, Need>();

            // for specification
            CreateMap<NeedSpecificationRequest, SpecificationParameters<Need>>();
        }
    }
}