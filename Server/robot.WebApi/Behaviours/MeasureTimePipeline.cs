using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace robot.WebApi.Behaviours
{
    public class MeasureTimePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = await next();
            var elapsed = stopWatch.Elapsed;
            Debug.WriteLine($"Tempo de execução do request {typeof(TRequest).FullName}: {elapsed}ms");
            return result;
        }
    }
}

