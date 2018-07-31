using AutoMapper;
using robot.Application.Features.Robo.ViewModels;
using robot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Application.Features
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Robot, RobotViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RobotId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RobotName))
                .ForMember(dest => dest.HeadAlign, opt => opt.MapFrom(src => (int)src.HeadAlign))
                .ForMember(dest => dest.HeadDirection, opt => opt.MapFrom(src => src.HeadDirection))
                .ForMember(dest => dest.LeftElbowPosition, opt => opt.MapFrom(src => src.LeftElbowPosition))
                .ForMember(dest => dest.RightElbowPosition, opt => opt.MapFrom(src => src.RightElbowPosition))
                .ForMember(dest => dest.LeftWristDirection, opt => opt.MapFrom(src => src.LeftWristDirection))
                .ForMember(dest => dest.RightWristDirection, opt => opt.MapFrom(src => src.RightWristDirection));
        }
    }
}
