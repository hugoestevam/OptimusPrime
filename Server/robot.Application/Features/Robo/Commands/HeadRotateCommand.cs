using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using robot.Domain.Results;
using System;

namespace robot.Application.Features.Robo.Commands
{
    public class HeadRotateCommand : IRequest<Result<Exception, int>>
    {
        public string HeadRotate { get; set; }
        [JsonIgnore]
        public string RobotId { get; set; }
    }

    public class HeadRotateCommandValidator : AbstractValidator<HeadRotateCommand>
    {
        public HeadRotateCommandValidator()
        {
            RuleFor(m => m.HeadRotate).NotEmpty();
            RuleFor(m => m.RobotId).NotEmpty();
        }
    }
}