using FluentValidation;
using MediatR;
using robot.Domain.Results;
using Unit = robot.Domain.Results.Unit;

namespace robot.Application.Features.Robo.Commands
{
    public class RobotDeleteCommand : IRequest<Result<Unit>>
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