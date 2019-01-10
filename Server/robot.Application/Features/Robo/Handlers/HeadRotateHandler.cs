using MediatR;
using robot.Application.Features.Robo.Commands;
using robot.Application.Notification;
using robot.Domain.Results;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class HeadRotateHandler : IRequestHandler<HeadRotateCommand, Result<Exception, int>>
    {
        private readonly IRobotRepository _repository;
        private readonly IMediator _mediator;

        public HeadRotateHandler(IRobotRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public Task<Result<Exception, int>> Handle(HeadRotateCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return Task.FromResult(findRobotCallback.Failure.Run<int>());

            return Task.FromResult(ProcessHeadRotate(command, findRobotCallback.Success)); 
        }

        private Result<Exception, int> ProcessHeadRotate(HeadRotateCommand command, RobotAgreggate robot)
        {
            Result<Exception, int> rotateCallback;
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

            // Publish Domain Events
            _mediator.PublishDomainEvents(robot.RaisedEvents());

            if (rotateCallback.IsSuccess)
                return PersistRobotState(robot, rotateCallback.Success);

            return rotateCallback.Failure;
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
