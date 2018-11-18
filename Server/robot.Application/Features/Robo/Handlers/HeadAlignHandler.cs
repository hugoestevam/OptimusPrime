using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain.Results;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class HeadAlignHandler : IRequestHandler<HeadAlignCommand, Result<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public HeadAlignHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Result<Exception, int>> Handle(HeadAlignCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                Task.FromResult(findRobotCallback.Failure.Run<int>());

            return Task.FromResult(ProcessHeadAlign(command, findRobotCallback.Success));
        }

        private Result<Exception, int> ProcessHeadAlign(HeadAlignCommand command, RobotAgreggate robot)
        {
            Result<Exception, Align> moveCallback;

            switch (command.HeadMove.ToLower())
            {
                case "top":
                    moveCallback = robot.MoveHeadForUp();
                    break;
                case "down":
                    moveCallback = robot.MoveHeadToBelow();
                    break;
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "Comando inválido.");
            }
           
            if (moveCallback.IsSuccess)
                return PersistRobotState(robot, (int)moveCallback.Success);

            return moveCallback.Failure;
        }

        private Result<Exception, int> PersistRobotState(RobotAgreggate robot, int state)
        {
            var updateCallback = _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return state;

            return updateCallback.Failure;
        }
    }
}
