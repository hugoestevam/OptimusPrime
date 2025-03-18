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

        public async Task<Result<Exception, int>> Handle(HeadRotateCommand command, CancellationToken cancellationToken)
        {
            var findRobotCallback = await _repository.Get(command.RobotId);

            if (!findRobotCallback.IsSuccess)
                return findRobotCallback.Failure;

            return await ProcessHeadRotate(command, findRobotCallback.Success); 
        }

        private async Task<Result<Exception, int>> ProcessHeadRotate(HeadRotateCommand command, RobotAgreggate robot)
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
                return await PersistRobotState(robot, rotateCallback.Success);

            return rotateCallback.Failure;
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
