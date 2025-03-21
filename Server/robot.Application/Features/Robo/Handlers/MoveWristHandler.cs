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
    public class MoveWristHandler : IRequestHandler<WristCommand, Result<int>>
    {
        private readonly IRobotRepository _repository;

        public MoveWristHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(WristCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = await _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Result<int>.Fail(findRobotCallback.Error);

            return await ProcessWristAction(command, findRobotCallback.Value);
        }

        private async Task<Result<int>> ProcessWristAction(WristCommand command, RobotAgreggate robot)
        {
            Result<int> actionCallback;
            switch (command.WristSide.ToLower())
            {
                case "left":
                    actionCallback = ExecuteActionInLeftWrist(robot, command.WristRotate);
                    break;
                case "right":
                    actionCallback = ExecuteActionInRightWrist(robot, command.WristRotate);
                    break;
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "WristSide possui comando inválido."));
            }

            if (actionCallback.IsSuccess)
                return await PersistRobotState(robot, actionCallback.Value);

            return Result<int>.Fail(actionCallback.Error);
        }

        private Result<int> ExecuteActionInLeftWrist(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "left":
                    return robot.LeftWristRotateToTheLeft();
                case "right":
                    return robot.LeftWristRotateToTheRight();
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "WristRotate possui comando inválido."));
            }
        }

        private Result<int> ExecuteActionInRightWrist(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "left":
                    return robot.RightWristRotateToTheLeft();
                case "right":
                    return robot.RightWristRotateToTheRight();
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "WristRotate possui comando inválido."));
            }
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
