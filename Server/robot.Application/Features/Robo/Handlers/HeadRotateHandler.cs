using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class HeadRotateHandler : IRequestHandler<HeadRotateCommand, Try<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public HeadRotateHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, int>> Handle(HeadRotateCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Task.FromResult(findRobotCallback.Failure.Run<int>());

            return Task.FromResult(ProcessHeadRotate(command, findRobotCallback.Result)); 
        }

        private Try<Exception, int> ProcessHeadRotate(HeadRotateCommand command, RobotAgreggate robot)
        {
            Try<Exception, int> rotateCallback;
            switch (command.HeadRotate.ToLower())
            {
                case "left":
                    rotateCallback = robot.RotateHeadToTheLeft();
                    break;
                case "right":
                    rotateCallback = robot.RotateHeadToTheRight();
                    break;
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "Comando inválido.");
            }

            if (rotateCallback.IsSuccess)
                return PersistRobotState(robot, rotateCallback.Result);

            return rotateCallback.Failure;
        }

        private Try<Exception, int> PersistRobotState(RobotAgreggate robot, int state)
        {
            var updateCallback = _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return state;

            return updateCallback.Failure;
        }
    }
}
