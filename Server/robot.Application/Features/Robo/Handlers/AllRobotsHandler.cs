using MediatR;
using robot.Application.Features.Robo.Queries;
using robot.Domain.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using robot.Domain.Features.Robo;
using OpenTelemetry.Trace;

namespace robot.Application.Features.Robo.Handlers
{
    public class Handler : IRequestHandler<Query, Result<List<RobotAgreggate>>>
    {
        private readonly IRobotRepository _repository;
        private readonly Tracer _tracer;

        public Handler(IRobotRepository repository)
        {
            _repository = repository;
            _tracer = ApplicationTelemetry.Instance.GetTracer("RobotHandler");
        }

        public async Task<Result<List<RobotAgreggate>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using (var span = _tracer.StartActiveSpan("HandleAllRobotsQuery"))
            {
                try
                {
                    var result = await _repository.GetAll();
                    span.SetAttribute("robot.count", result.Value?.Count ?? 0);
                    return result;
                }
                catch (Exception ex)
                {
                    span.SetStatus(Status.Error.WithDescription(ex.Message));
                    return Result<List<RobotAgreggate>>.Fail(ex);
                }
            }
        }
    }
}
