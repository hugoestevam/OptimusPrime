using MediatR;
using robot.Application.Features.Robo.Queries;
using robot.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class FindRobotHandler : IRequestHandler<RobotQuery, Try<Exception, RobotAgreggate>>
    {
        private readonly IRobotRepository _repository;

        public FindRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, RobotAgreggate>> Handle(RobotQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.Get(query.RobotId));
        }
    }
}
