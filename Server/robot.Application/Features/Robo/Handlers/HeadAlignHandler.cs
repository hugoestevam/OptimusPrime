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
    public class HeadAlignHandler : IRequestHandler<HeadAlignCommand, Result<int>>
    {
        private readonly IRobotRepository _repository;

        public HeadAlignHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(HeadAlignCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = await _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Result<int>.Fail(findRobotCallback.Error);

            return await ProcessHeadAlign(command, findRobotCallback.Value);
        }

        private async Task<Result<int>> ProcessHeadAlign(HeadAlignCommand command, RobotAgreggate robot)
        {
            Result<Align> moveCallback;

            switch (command.HeadMove.ToLower())
            {
                case "top":
                    moveCallback = robot.MoveHeadForUp();
                    break;
                case "down":
                    moveCallback = robot.MoveHeadToBelow();
                    break;
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "Comando inválido."));
            }
           
            if (moveCallback.IsSuccess)
                return await PersistRobotState(robot, (int)moveCallback.Value);

            return Result<int>.Fail(moveCallback.Error);
        }

        private async Task<Result<int>> PersistRobotState(RobotAgreggate robot, int state)
        {
            var updateCallback = await _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return Result<int>.Success(state);

            return Result<int>.Fail(updateCallback.Error);
        }
    }
}
