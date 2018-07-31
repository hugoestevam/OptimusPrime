using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class HeadRotateCommand : IRequest<Try<Exception, int>>
    {
        public string HeadRotate { get; set; }
        [JsonIgnore]
        public string RobotId { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<HeadRotateCommand>
        {
            public Validator()
            {
                RuleFor(m => m.HeadRotate).NotEmpty();
                RuleFor(m => m.RobotId).NotEmpty();
            }
        }
    }
}