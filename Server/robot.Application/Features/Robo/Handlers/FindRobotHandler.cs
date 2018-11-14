using FluentValidation;
using MediatR;
using robot.Application.Features.Robo.Queries;
using robot.Domain;
using robot.Domain.Contract;
using robot.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace robot.Application.Features.Robo.Handlers
{
    public class FindRobotHandler : IRequestHandler<RobotQuery, Try<Exception, Robot>>
    {
        private readonly IRobotRepository _repository;

        public FindRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, Robot>> Handle(RobotQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.Get(query.RobotId));
        }
    }
}
