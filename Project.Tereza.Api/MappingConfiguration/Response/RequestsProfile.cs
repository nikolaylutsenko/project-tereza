using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Tereza.Core;
using Project.Tereza.Requests.Requests;

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
        }
    }
}