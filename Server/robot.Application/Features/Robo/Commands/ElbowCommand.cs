using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Results;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class ElbowCommand : IRequest<Result<int>>
    {
        public string ElbowSide { get; set; }
        public string ElbowAction { get; set; }
        [JsonIgnore]
        public long RobotId { get; set; }
       
    }

    public class ElbowCommandValidator : AbstractValidator<ElbowCommand>
    {
        public ElbowCommandValidator()
        {
            RuleFor(m => m.ElbowSide).NotEmpty();
            RuleFor(m => m.ElbowAction).NotEmpty();
            RuleFor(m => m.RobotId).NotEqual(0);
        }
    }
}