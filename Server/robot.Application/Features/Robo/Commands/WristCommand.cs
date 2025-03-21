using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Results;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class WristCommand : IRequest<Result<int>>
    {
        public string WristSide { get; set; }
        public string WristRotate { get; set; }
        [JsonIgnore]
        public long RobotId { get; set; }
    }

    public class WristCommandValidator : AbstractValidator<WristCommand>
    {
        public WristCommandValidator()
        {
            RuleFor(m => m.WristSide).NotEmpty();
            RuleFor(m => m.WristRotate).NotEmpty();
            RuleFor(m => m.RobotId).NotEqual(0);
        }
    }
}