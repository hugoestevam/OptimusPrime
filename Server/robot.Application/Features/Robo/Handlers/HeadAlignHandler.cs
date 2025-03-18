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

        public async Task<Result<Exception, int>> Handle(HeadAlignCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = await _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return findRobotCallback.Failure;

            return await ProcessHeadAlign(command, findRobotCallback.Success);
        }

        private async Task<Result<Exception, int>> ProcessHeadAlign(HeadAlignCommand command, RobotAgreggate robot)
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
                return await PersistRobotState(robot, (int)moveCallback.Success);

            return moveCallback.Failure;
        }

        private async Task<Result<Exception, int>> PersistRobotState(RobotAgreggate robot, int state)
        {
            var updateCallback = await _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return state;

            return updateCallback.Failure;
        }
    }
}
