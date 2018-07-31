using MediatR;
using robot.Application.Features.Robo.Queries;
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
    public class Handler : IRequestHandler<Query, Try<Exception, List<Robot>>>
    {
        private readonly IRobotRepository _repository;

        public Handler(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<Try<Exception, List<Robot>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}
