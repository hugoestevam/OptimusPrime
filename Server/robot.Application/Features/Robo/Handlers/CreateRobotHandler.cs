using FluentValidation;
using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain;
using robot.Domain.Contract;
using robot.Domain.Exceptions;
using robot.Domain.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robot.Application.Features.Robo.Handlers
{
    public class CreateRobotHandler : IRequestHandler<RobotCreateCommand, Try<Exception, Robot>>
    {
        private readonly IRobotRepository _repository;

        public CreateRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, Robot>> Handle(RobotCreateCommand command, CancellationToken cancellationToken)
        {
            Creator creator = new ConcreteCreatorRobot();

            var robot = creator.MakeARobot();
            robot.RobotName = command.RobotName;

            return Task.FromResult(_repository.Add(robot));
        }
    }
}
