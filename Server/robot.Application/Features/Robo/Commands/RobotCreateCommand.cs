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

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<RobotCreateCommand>
        {
            public Validator()
            {
                RuleFor(m => m.RobotName).NotEmpty();
            }
        }
    }
}