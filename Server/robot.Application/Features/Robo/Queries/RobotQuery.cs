using FluentValidation;
using FluentValidation.Results;
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

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<RobotQuery>
        {
            public Validator()
            {
                RuleFor(m => m.RobotId).NotEmpty();
            }
        }
    }
}