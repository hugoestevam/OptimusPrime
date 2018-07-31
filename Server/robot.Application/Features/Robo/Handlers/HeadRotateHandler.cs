﻿using FluentValidation;
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
    public class HeadRotateHandler : IRequestHandler<HeadRotateCommand, Try<Exception, int>>
    {
        private readonly IRobotRepository _repository;

        public HeadRotateHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, int>> Handle(HeadRotateCommand command, CancellationToken cancellationToken)
        {
            var result = command.Validate();

            if (!result.IsValid)
                return Task
                        .FromResult(new ValidationException(result.Errors)
                        .Run<int>());

            var findRobotCallback = _repository.Get(command.RobotId);

            if (findRobotCallback.IsSuccess)
                return Task.FromResult(ProcessHeadRotate(command, findRobotCallback.Result));

            return Task.FromResult(findRobotCallback.Failure.Run<int>());
        }

        private Try<Exception, int> ProcessHeadRotate(HeadRotateCommand command, Robot robot)
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

        private Try<Exception, int> PersistRobotState(Robot robot, int state)
        {
            var updateCallback = _repository.Update(robot);

            if (updateCallback.IsSuccess)
                return state;

            return updateCallback.Failure;
        }
    }
}
