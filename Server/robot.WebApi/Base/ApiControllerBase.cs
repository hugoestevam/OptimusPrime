using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using robot.Domain.Results;
using robot.WebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using robot.Domain.Exceptions;
using Microsoft.AspNetCore.Components;

namespace robot.WebApi.Base
{
    public class ApiControllerBase: Controller
    {
        [Inject]
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Manuseia o callback. Valida se é necessário retornar erro ou o próprio TSuccess
        /// </summary> 
        /// <typeparam name="TSuccess"></typeparam>
        /// <param name="callback">Objeto Result utilizado nas chamadas.</param>
        /// <returns></returns>
        public IActionResult HandleCommand<TSuccess>(Result<TSuccess> callback)
        {
            return callback.IsSuccess ? Ok(callback.Value) : HandleFailure(callback.Error);
        }

        /// <summary>
        /// Manuseia o callback. Valida se é necessário retornar erro ou o próprio TSuccess convertido para TResult
        /// </summary> 
        /// <typeparam name="TSource">Tipo de Origem</typeparam>
        /// <typeparam name="TResult">Tipo de Destino (ViewModel)</typeparam>
        /// <param name="callback">Objeto Result utilizado nas chamadas.</param>
        /// <returns></returns>
        public IActionResult HandleQuery<TSource, TResult>(Result<TSource> callback)
        {
            return callback.IsSuccess ? Ok(Mapper.Map<TSource, TResult>(callback.Value)) : HandleFailure(callback.Error);
        }

        /// <summary>
        /// Manuseia o callback. Valida se é necessário retornar erro ou a própria Lista TResult.
        /// </summary>
        /// <typeparam name="TSource">Tipo do obj de origem (domínio)</typeparam>
        /// <typeparam name="TResult">Tipo de retorno objQuery</typeparam>
        /// <param name="callback">Result<List<TSource>></param>
        /// <returns>Result(TResult)</returns>
        public IActionResult HandleQueryList<TSource, TResult>(Result<List<TSource>> callback)
        {
            if (!callback.IsSuccess)
                return HandleFailure(callback.Error);

            var query = callback.Value;

            var result = query.AsQueryable().ProjectTo<TResult>(Mapper.ConfigurationProvider);

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
