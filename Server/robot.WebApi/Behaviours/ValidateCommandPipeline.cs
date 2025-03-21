using FluentValidation;
using MediatR;
using robot.Domain.Results;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace robot.WebApi.Behaviours
{   
    /// <summary>
    /// Pipeline que valida todos os Commands, que implementam a interface IRequest do MediatR, antes de chamar a execução do handler.
    /// </summary>
    public class ValidateCommandPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidateCommandPipeline(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                var validationException = new ValidationException(failures);
                return Result<TResponse>.Fail(validationException);
            }

            return await next();
        }
    }
}