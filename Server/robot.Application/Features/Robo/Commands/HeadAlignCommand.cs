using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain;
using robot.Domain.Exceptions;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class HeadAlignCommand : IRequest<Try<Exception, int>>
    {
        public string HeadMove { get; set; }
        [JsonIgnore]
        public string RobotId { get; set; }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        private class Validator : AbstractValidator<HeadAlignCommand>
        {
            public Validator()
            {
                RuleFor(m => m.HeadMove).NotEmpty();
                RuleFor(m => m.RobotId).NotEmpty();
            }
        }
    }
}