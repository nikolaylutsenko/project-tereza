using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Tereza.Core;
using Project.Tereza.Responses;

namespace Project.Tereza.Api.MappingConfiguration.Response
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<Need, NeedResponse>();
        }
    }
}