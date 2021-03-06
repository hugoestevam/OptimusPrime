﻿using FluentValidation;
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
    public class ValidateCommandPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<Exception, TResponse>>
        where TRequest : IRequest<Result<Exception, TResponse>>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidateCommandPipeline(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<Result<Exception, TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<Exception, TResponse>> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                return new ValidationException(failures);
            }

            return await next();
        }
    }
}