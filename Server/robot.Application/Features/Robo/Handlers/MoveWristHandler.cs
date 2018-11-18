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
    public class MoveWristHandler : IRequestHandler<WristCommand, Result<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public MoveWristHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Result<Exception, int>> Handle(WristCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Task.FromResult(findRobotCallback.Failure.Run<int>());

            return Task.FromResult(ProcessWristAction(command, findRobotCallback.Success));
        }

        private Result<Exception, int> ProcessWristAction(WristCommand command, RobotAgreggate robot)
        {
            Result<Exception, int> actionCallback;
            switch (command.WristSide.ToLower())
            {
                case "left":
                    actionCallback = ExecuteActionInLeftWrist(robot, command.WristRotate);
                    break;
                case "right":
                    actionCallback = ExecuteActionInRightWrist(robot, command.WristRotate);
                    break;
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "WristSide possui comando inválido.");
            }

            if (actionCallback.IsSuccess)
                return PersistRobotState(robot, actionCallback.Success);

            return actionCallback.Failure;
        }

        private Result<Exception, int> ExecuteActionInLeftWrist(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "left":
                    return robot.LeftWristRotateToTheLeft();
                case "right":
                    return robot.LeftWristRotateToTheRight();
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "WristRotate possui comando inválido.");
            }
        }

        private Result<Exception, int> ExecuteActionInRightWrist(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "left":
                    return robot.RightWristRotateToTheLeft();
                case "right":
                    return robot.RightWristRotateToTheRight();
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "WristRotate possui comando inválido.");
            }
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
