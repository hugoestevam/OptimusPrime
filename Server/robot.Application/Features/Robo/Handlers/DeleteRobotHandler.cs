using FluentValidation;
using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain;
using robot.Domain.Contract;
using robot.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robot.Application.Features.Robo.Handlers
{
    public class DeleteRobotHandler : IRequestHandler<RobotDeleteCommand, Try<Exception, Result>>
    {
        private readonly IRobotRepository _repository;

        public DeleteRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, Result>> Handle(RobotDeleteCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.Delete(command.RobotId));
        }
    }
}
