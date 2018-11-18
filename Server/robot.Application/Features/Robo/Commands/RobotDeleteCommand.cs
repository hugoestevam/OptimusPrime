using FluentValidation;
using FluentValidation.Results;
using MediatR;
using robot.Domain;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class RobotDeleteCommand : IRequest<Try<Exception, Result>>
    {
        public string RobotId { get; set; }

        public RobotDeleteCommand(string id)
        {
            RobotId = id;
        }
    }

    public class RobotDeleteCommandValidator : AbstractValidator<RobotDeleteCommand>
    {
        public RobotDeleteCommandValidator()
        {
            RuleFor(m => m.RobotId).NotEmpty();
        }
    }
}