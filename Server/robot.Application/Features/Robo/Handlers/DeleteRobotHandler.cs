using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain.Results;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;
using Unit = robot.Domain.Results.Unit;

namespace robot.Application.Features.Robo.Handlers
{
    public class DeleteRobotHandler : IRequestHandler<RobotDeleteCommand, Result<Unit>>
    {
        private readonly IRobotRepository _repository;

        public DeleteRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(RobotDeleteCommand command, CancellationToken cancellationToken)
        {
            return await _repository.Delete(command.RobotId);
        }
    }
}
