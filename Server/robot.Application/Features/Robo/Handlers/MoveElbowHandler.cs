using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain.Results;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class MoveElbowHandler : IRequestHandler<ElbowCommand, Result<int>>
    {
        private readonly IRobotRepository _repository;

        public MoveElbowHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(ElbowCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = await _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Result<int>.Fail(findRobotCallback.Error);

            return await ProcessElbowAction(command, findRobotCallback.Value);
        }

        private async Task<Result<int>> ProcessElbowAction(ElbowCommand command, RobotAgreggate robot)
        {
            Result<int> actionCallback;
            switch (command.ElbowSide.ToLower())
            {
                case "left":
                    actionCallback = ExecuteActionInLeftElbow(robot, command.ElbowAction);
                    break;
                case "right":
                    actionCallback = ExecuteActionInRightElbow(robot, command.ElbowAction);
                    break;
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "ElbowSide possui comando inválido."));
            }

            if (actionCallback.IsSuccess)
                return await PersistRobotState(robot, actionCallback.Value);

            return Result<int>.Fail(actionCallback.Error);
        }

        private Result<int> ExecuteActionInLeftElbow(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "collapse":
                    return robot.LeftElbowCollapse();
                case "expand":
                    return robot.LeftElbowExpand();
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "ElbowAction possui comando inválido."));
            }
        }

        private Result<int> ExecuteActionInRightElbow(RobotAgreggate robot, string action)
        {
            switch (action.ToLower())
            {
                case "collapse":
                    return robot.RightElbowCollapse();
                case "expand":
                    return robot.RightElbowExpand();
                default:
                    return Result<int>.Fail(new BussinessException(ErrorCodes.BadRequest, "ElbowAction possui comando inválido."));
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
