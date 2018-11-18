using FluentValidation;
using MediatR;
using robot.Domain.Results;
using System;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Queries
{
    public class RobotQuery : IRequest<Result<Exception, RobotAgreggate>>
    {
        public string RobotId { get; set; }
        public RobotQuery(string id)
        {
            RobotId = id;
        }
    }

    public class RobotQueryValidator : AbstractValidator<RobotQuery>
    {
        public RobotQueryValidator()
        {
            RuleFor(m => m.RobotId).NotEmpty();
        }
    }
}