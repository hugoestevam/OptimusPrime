using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain;
using robot.Domain.Results;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class HeadAlignCommand : IRequest<Result<Exception, int>>
    {
        public string HeadMove { get; set; }
        [JsonIgnore]
        public long RobotId { get; set; }
       
    }

    public class HeadAlignCommandValidator : AbstractValidator<HeadAlignCommand>
    {
        public HeadAlignCommandValidator()
        {
            RuleFor(m => m.HeadMove).NotEmpty();
            RuleFor(m => m.RobotId).NotEqual(0);
        }
    }
}