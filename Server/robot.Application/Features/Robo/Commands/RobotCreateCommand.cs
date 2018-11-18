using FluentValidation;
using FluentValidation.Results;
using MediatR;
using robot.Domain;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class RobotCreateCommand : IRequest<Try<Exception, Robot>>
    {
        public string RobotName { get; set; }
    }

    public class RobotCreateCommandValidator : AbstractValidator<RobotCreateCommand>
    {
        public RobotCreateCommandValidator()
        {
            RuleFor(m => m.RobotName).NotEmpty();
        }
    }
}