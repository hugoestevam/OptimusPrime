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
    public class HeadAlignHandler : IRequestHandler<HeadAlignCommand, Try<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public HeadAlignHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, int>> Handle(HeadAlignCommand command, CancellationToken cancellationToken)
        {
            var result = command.Validate();

            if (!result.IsValid)
                return Task
                        .FromResult(new ValidationException(result.Errors)
                        .Run<int>());
            
            var findRobotCallback = _repository.Get(command.RobotId);

            if (findRobotCallback.IsSuccess)
                return Task.FromResult(ProcessHeadAlign(command, findRobotCallback.Result));

            return Task.FromResult(findRobotCallback.Failure.Run<int>());
        }

        private Try<Exception, int> ProcessHeadAlign(HeadAlignCommand command, Robot robot)
        {
            Try<Exception, Align> moveCallback;
            switch (command.HeadMove.ToLower())
            {
                case "top":
                    moveCallback = robot.MoveHeadForUp();
                    break;
                case "down":
                    moveCallback = robot.MoveHeadToBelow();
                    break;
                default:
                    return new BussinessException(ErrorCodes.BadRequest, "Comando inválido.");
            }

            if (moveCallback.IsSuccess)
                return PersistRobotState(robot, (int)moveCallback.Result);

            return moveCallback.Failure;
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
