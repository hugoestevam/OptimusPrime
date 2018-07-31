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
    public class MoveWristHandler : IRequestHandler<WristCommand, Try<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public MoveWristHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, int>> Handle(WristCommand command, CancellationToken cancellationToken)
        {
            var result = command.Validate();

            if (!result.IsValid)
                return Task
                        .FromResult(new ValidationException(result.Errors)
                        .Run<int>());

            var findRobotCallback = _repository.Get(command.RobotId);

            if (findRobotCallback.IsSuccess)
                return Task.FromResult(ProcessWristAction(command, findRobotCallback.Result));

            return Task.FromResult(findRobotCallback.Failure.Run<int>());
        }

        private Try<Exception, int> ProcessWristAction(WristCommand command, Robot robot)
        {
            Try<Exception, int> actionCallback;
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
                return PersistRobotState(robot, actionCallback.Result);

            return actionCallback.Failure;
        }

        private Try<Exception, int> ExecuteActionInLeftWrist(Robot robot, string action)
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

        private Try<Exception, int> ExecuteActionInRightWrist(Robot robot, string action)
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

        private Try<Exception, int> PersistRobotState(Robot robot, int state)
        {
            var updateCallback = _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return state;

            return updateCallback.Failure;
        }
    }
}
