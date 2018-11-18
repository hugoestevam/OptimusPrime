using FluentValidation;
using MediatR;
using robot.Domain;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Queries
{
    public class RobotQuery : IRequest<Try<Exception, Robot>>
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