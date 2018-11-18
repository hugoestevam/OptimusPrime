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