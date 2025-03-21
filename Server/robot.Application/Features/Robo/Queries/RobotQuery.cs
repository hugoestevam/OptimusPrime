using FluentValidation;
using MediatR;
using robot.Domain.Results;
using System;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Queries
{
    public class RobotQuery : IRequest<Result<RobotAgreggate>>
    {
        public long RobotId { get; set; }
        public RobotQuery(long id)
        {
            RobotId = id;
        }
    }

    public class RobotQueryValidator : AbstractValidator<RobotQuery>
    {
        public RobotQueryValidator()
        {
            RuleFor(m => m.RobotId).NotEqual(0);
        }
    }
}