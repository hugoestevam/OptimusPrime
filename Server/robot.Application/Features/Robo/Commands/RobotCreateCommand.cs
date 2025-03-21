using FluentValidation;
using MediatR;
using robot.Domain.Results;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Commands
{
    public class RobotCreateCommand : IRequest<Result<RobotAgreggate>>
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