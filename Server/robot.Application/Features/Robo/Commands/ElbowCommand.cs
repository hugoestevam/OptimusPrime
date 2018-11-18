using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class ElbowCommand : IRequest<Try<Exception, int>>
    {
        public string ElbowSide { get; set; }
        public string ElbowAction { get; set; }
        [JsonIgnore]
        public string RobotId { get; set; }
       
    }

    public class ElbowCommandValidator : AbstractValidator<ElbowCommand>
    {
        public ElbowCommandValidator()
        {
            RuleFor(m => m.ElbowSide).NotEmpty();
            RuleFor(m => m.ElbowAction).NotEmpty();
            RuleFor(m => m.RobotId).NotEmpty();
        }
    }
}