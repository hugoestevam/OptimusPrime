using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace robot.WebApi.Behaviours
{
    public class MeasureTimePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = next();
            var elapsed = stopWatch.Elapsed;
            Debug.WriteLine($"Tempo de execução do request {typeof(TRequest).FullName}: {elapsed}ms");
            return result;
        }
    }
}

