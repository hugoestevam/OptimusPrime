using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Results;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class WristCommand : IRequest<Result<Exception, int>>
    {
        public string WristSide { get; set; }
        public string WristRotate { get; set; }
        [JsonIgnore]
        public string RobotId { get; set; }
    }

    public class WristCommandValidator : AbstractValidator<WristCommand>
    {
        public WristCommandValidator()
        {
            RuleFor(m => m.WristSide).NotEmpty();
            RuleFor(m => m.WristRotate).NotEmpty();
            RuleFor(m => m.RobotId).NotEmpty();
        }
    }
}