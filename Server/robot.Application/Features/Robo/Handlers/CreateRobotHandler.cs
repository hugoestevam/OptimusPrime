using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain.Results;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class CreateRobotHandler : IRequestHandler<RobotCreateCommand, Result<RobotAgreggate>>
    {
        private readonly IRobotRepository _repository;

        public CreateRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<RobotAgreggate>> Handle(RobotCreateCommand command, CancellationToken cancellationToken)
        {
            AbstractRobotFactory creator = new ConcreteRobotFactory();

            var robot = creator.MakeARobot();
            robot.RobotName = command.RobotName;

            return await _repository.Add(robot);
        }
    }
}
