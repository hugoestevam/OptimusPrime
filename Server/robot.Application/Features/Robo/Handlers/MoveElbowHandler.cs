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
    public class MoveElbowHandler : IRequestHandler<ElbowCommand, Try<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public MoveElbowHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, int>> Handle(ElbowCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Task.FromResult(findRobotCallback.Failure.Run<int>());

            return Task.FromResult(ProcessElbowAction(command, findRobotCallback.Result));
        }

        private Try<Exception, int> ProcessElbowAction(ElbowCommand command, Robot robot)
        {
            Try<Exception, int> actionCallback;
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
                return PersistRobotState(robot, actionCallback.Result);

            return actionCallback.Failure;
        }

        private Try<Exception, int> ExecuteActionInLeftElbow(Robot robot, string action)
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

        private Try<Exception, int> ExecuteActionInRightElbow(Robot robot, string action)
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

        private Try<Exception, int> PersistRobotState(Robot robot, int state)
        {
            var updateCallback = _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return state;

            return updateCallback.Failure;
        }
    } 
}
