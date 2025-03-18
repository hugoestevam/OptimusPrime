using MediatR;
using robot.Application.Features.Robo.Queries;
using robot.Domain.Results;
using System;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class FindRobotHandler : IRequestHandler<RobotQuery, Result<Exception, RobotAgreggate>>
    {
        private readonly IRobotRepository _repository;

        public FindRobotHandler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Result<Exception, RobotAgreggate>> Handle(RobotQuery query, CancellationToken cancellationToken)
        {
            return _repository.Get(query.RobotId);
        }
    }
}
