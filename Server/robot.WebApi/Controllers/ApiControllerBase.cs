using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using robot.Domain.Exceptions;
using robot.WebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace robot.WebApi.Controllers
{
    public class ApiControllerBase: Controller
    {
        // <summary>
        /// Manuseia o callback. Valida se é necessário retornar erro ou o próprio TSuccess
        /// </summary> 
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccess"></typeparam>
        /// <param name="callback">Objeto Try utilizado nas chamadas.</param>
        /// <returns></returns>
        public IActionResult HandleCommand<TFailure, TSuccess>(Try<TFailure, TSuccess> callback) where TFailure : Exception
        {
            return callback.IsFailure ? HandleFailure(callback.Failure) : Ok(callback.Result);
        }

        /// <summary>
        /// Manuseia o callback. Valida se é necessário retornar erro ou o próprio TSuccess convertido para TResult
        /// </summary> 
        /// <typeparam name="TSource">Tipo de Origem</typeparam>
        /// <typeparam name="TResult">Tipo de Destino (ViewModel)</typeparam>
        /// <param name="callback">Objeto Try utilizado nas chamadas.</param>
        /// <returns></returns>
        public IActionResult HandleQuery<TSource, TResult>(Try<Exception, TSource> callback)
        {
            return callback.IsFailure ? HandleFailure(callback.Failure) : Ok(Mapper.Map<TSource, TResult>(callback.Result));
        }

        /// <summary>
        /// Manuseia o callback. Valida se é necessário retornar erro ou a própria Lista TResult.
        /// </summary>
        /// <typeparam name="TSource">Tipo do obj de origem (domínio)</typeparam>
        /// <typeparam name="TResult">Tipo de retorno objQuery</typeparam>
        /// <param name="callback">List(TSource)</param>
        /// <returns>Result(TResult)</returns>
        public IActionResult HandleQueryList<TSource, TResult>(Try<Exception, List<TSource>> callback)
        {
            if (callback.IsFailure)
                return HandleFailure(callback.Failure);

            var query = callback.Result;

            var result = query.AsQueryable().ProjectTo<TResult>();

            return Ok(result);
        }

        /// <summary>
        /// Verifica a exceção passada por parametro para passar o StatusCode correto para o frontend.
        /// </summary>
        /// <typeparam name="T">Qualquer classe que herde de Exception</typeparam>
        /// <param name="exceptionToHandle">obj de exceção</param>
        public IActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            if (exceptionToHandle is ValidationException)
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), (exceptionToHandle as ValidationException).Errors);

            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);

            return exceptionToHandle is BussinessException ?
                StatusCode(HttpStatusCode.BadRequest.GetHashCode(), exceptionPayload) :
                StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), exceptionPayload);
        }
    }
}
