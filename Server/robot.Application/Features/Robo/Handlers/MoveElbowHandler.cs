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
    public class MoveElbowHandler : IRequestHandler<ElbowCommand, Result<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public MoveElbowHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Result<Exception, int>> Handle(ElbowCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Task.FromResult(findRobotCallback.Failure.Run<int>());

            return Task.FromResult(ProcessElbowAction(command, findRobotCallback.Success));
        }

        private Result<Exception, int> ProcessElbowAction(ElbowCommand command, RobotAgreggate robot)
        {
            Result<Exception, int> actionCallback;
            switch (command.ElbowSide.ToLower())
            {
                case "left":
                    actionCallback = ExecuteActionInLeftElbow(robot, command.ElbowAction);
                    break;
                case "right":
                    actionCallback = ExecuteActionInRightElbow(robot, command.ElbowAction);
                    break;
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "ElbowSide possui comando inválido.");
            }

            if (actionCallback.IsSuccess)
                return PersistRobotState(robot, actionCallback.Success);

            return actionCallback.Failure;
        }

        private Result<Exception, int> ExecuteActionInLeftElbow(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "collapse":
                    return robot.LeftElbowCollapse();
                case "expand":
                    return robot.LeftElbowExpand();
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "ElbowAction possui comando inválido.");
            }
        }

        private Result<Exception, int> ExecuteActionInRightElbow(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "collapse":
                    return robot.RightElbowCollapse();
                case "expand":
                    return robot.RightElbowExpand();
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "ElbowAction possui comando inválido.");
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
