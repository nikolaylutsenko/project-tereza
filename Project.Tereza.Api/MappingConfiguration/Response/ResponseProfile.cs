using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Project.Tereza.Core;
using Project.Tereza.Responses.Responses;

namespace Project.Tereza.Api.MappingConfiguration.Response
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            // Needs
            CreateMap<Need, NeedResponse>();

            // mapping error message into response
            CreateMap<ValidationFailure, ErrorResponse>()
                .ForMember(dest => dest.PropertyName, src => src.MapFrom(opt => opt.PropertyName))
                .ForMember(dest => dest.Message, src => src.MapFrom(opt => opt.ErrorMessage));
        }
    }
}