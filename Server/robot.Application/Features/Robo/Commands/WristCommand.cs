using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class WristCommand : IRequest<Try<Exception, int>>
    {
        public string WristSide { get; set; }
        public string WristRotate { get; set; }
        [JsonIgnore]
        public string RobotId { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<WristCommand>
        {
            public Validator()
            {
                RuleFor(m => m.WristSide).NotEmpty();
                RuleFor(m => m.WristRotate).NotEmpty();
                RuleFor(m => m.RobotId).NotEmpty();
            }
        }
    }
}