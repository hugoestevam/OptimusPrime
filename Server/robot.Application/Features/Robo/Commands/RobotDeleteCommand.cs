using FluentValidation;
using FluentValidation.Results;
using MediatR;
using robot.Domain;
using robot.Domain.Results;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class RobotDeleteCommand : IRequest<Result<Exception, Domain.Results.Unit>>
    {
        public long RobotId { get; set; }

        public RobotDeleteCommand(long id)
        {
            RobotId = id;
        }
    }

    public class RobotDeleteCommandValidator : AbstractValidator<RobotDeleteCommand>
    {
        public RobotDeleteCommandValidator()
        {
            RuleFor(m => m.RobotId).NotEqual(0);
        }
    }
}