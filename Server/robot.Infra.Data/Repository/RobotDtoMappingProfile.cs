using AutoMapper;
using robot.Domain.Features.Robo;
using robot.Infra.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace robot.Infra.Data.Repository
{
    public class RobotDtoMappingProfile : Profile
    {
        public RobotDtoMappingProfile()
        {
            CreateMap<RobotAgreggate, RobotDao>()
             .ForMember(dest => dest.RobotId, opt => opt.MapFrom(src => src.RobotId))
             .ForMember(dest => dest.RobotName, opt => opt.MapFrom(src => src.RobotName))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
             .ForMember(dest => dest.HeadAlign, opt => opt.MapFrom(src => (int)src.HeadAlign))
             .ForMember(dest => dest.HeadDirection, opt => opt.MapFrom(src => src.HeadDirection))
             .ForMember(dest => dest.LeftElbowPosition, opt => opt.MapFrom(src => src.LeftElbowPosition))
             .ForMember(dest => dest.RightElbowPosition, opt => opt.MapFrom(src => src.RightElbowPosition))
             .ForMember(dest => dest.LeftWristDirection, opt => opt.MapFrom(src => src.LeftWristDirection))
             .ForMember(dest => dest.RightWristDirection, opt => opt.MapFrom(src => src.RightWristDirection));
            
        }
    }
}
