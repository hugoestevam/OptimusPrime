using MediatR;
using robot.Application.Features.Robo.Queries;
using robot.Domain.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Handlers
{
    public class Handler : IRequestHandler<Query, Result<Exception, List<RobotAgreggate>>>
    {
        private readonly IRobotRepository _repository;

        public Handler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Result<Exception, List<RobotAgreggate>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}
