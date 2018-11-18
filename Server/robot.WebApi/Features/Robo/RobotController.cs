using MediatR;
using Microsoft.AspNetCore.Mvc;
using robot.Application.Features.Robo.Commands;
using robot.Application.Features.Robo.Queries;
using robot.Domain.Exceptions;
using robot.WebApi.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using robot.Domain;
using robot.Domain.Features.Robo;
using robot.WebApi.Base;

namespace robot.WebApi.Features.Robo
{
    [Route("v1/[controller]")]
    public class RobotController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public RobotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region HTTP GET
        /// <summary>
        /// Busca os robos cadastrados no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /v1/Robot
        ///
        /// </remarks>
        /// <returns>Uma lista com os Robos</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [HttpGet]
        [ProducesResponseType(typeof(List<RobotViewModel>), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new Query());

            return HandleQueryList<RobotAgreggate, RobotViewModel>(response);
        }

        /// <summary>
        /// Busca um determinado robo pelo seu identificador.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /v1/Robot/{id}
        ///
        /// </remarks>
        /// <returns>Retorno um Robo</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [ProducesResponseType(typeof(RobotViewModel), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _mediator.Send(new RobotQuery(id));

            return HandleQuery<RobotAgreggate, RobotViewModel>(response);
        }

        #endregion

        #region HTTP POST 
        /// <summary>
        /// Cria um novo Robo no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /v1/Robot
        ///     
        ///     {
        ///        "RobotName": "Nome do Robo"
        ///     }
        ///    
        /// </remarks>
        /// <returns>Retorna o Robo criado, no seu estado padrão</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [HttpPost]
        [ProducesResponseType(typeof(RobotViewModel), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        public async Task<IActionResult> Post([FromBody]RobotCreateCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleCommand(result);
        }

        #endregion

        #region HTTP PUT
        /// <summary>
        /// Atualiza o alinhamento da cabeça do Robo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /v1/Robot/{id}/HeadAlign
        ///     
        ///     {
        ///        "headMove": "Top" //[Top, Down]
        ///     }
        ///    
        /// </remarks>
        /// <returns>Retorna o status a alteração solicitada</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        [HttpPut("{id}/HeadAlign")]
        public async Task<IActionResult> PutHeadAlign(string id, [FromBody]HeadAlignCommand command)
        {
            command.RobotId = id;

            var result = await _mediator.Send(command);

            return HandleCommand(result);
        }

        /// <summary>
        /// Atualiza a rotação da cabeça do Robo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /v1/Robot/{id}/HeadRotate
        ///     
        ///     {
        ///        "headRotate": "Left" //[Left, Right]
        ///     }
        ///    
        /// </remarks>
        /// <returns>Retorna o status a alteração solicitada</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        [HttpPut("{id}/HeadRotate")]
        public async Task<IActionResult> PutHeadRotate(string id, [FromBody]HeadRotateCommand command)
        {
            command.RobotId = id;

            var result = await _mediator.Send(command);

            return HandleCommand(result);
        }

        /// <summary>
        /// Atualiza a posição do colotovelo do Robo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /v1/Robot/{id}/Elbow
        ///     
        ///     {
        ///        "elbowSide": "Left", //[Left, Right]
        ///        "elbowAction": "Collapse", //[Collapse, Expand]
        ///     }
        ///    
        /// </remarks>
        /// <returns>Retorna o status a alteração solicitada</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        [HttpPut("{id}/Elbow")]
        public async Task<IActionResult> PutElbow(string id, [FromBody]ElbowCommand command)
        {
            command.RobotId = id;

            var result = await _mediator.Send(command);

            return HandleCommand(result);
        }

        /// <summary>
        /// Atualiza a rotação do pulso do Robo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /v1/Robot/{id}/Wrist
        ///     
        ///     {
        ///        "wristSide": "Left", //[Left, Right]
        ///        "wristRotate": "Left", //[Left, Right]
        ///     }
        ///    
        /// </remarks>
        /// <returns>Retorna o status a alteração solicitada</returns>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        [HttpPut("{id}/Wrist")]
        public async Task<IActionResult> PutRightElbow(string id, [FromBody]WristCommand command)
        {
            command.RobotId = id;

            var result = await _mediator.Send(command);

            return HandleCommand(result);
        }

        #endregion

        #region HTTP DELETE
        /// <summary>
        /// Deleta um Robo do sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /v1/Robot/{id}
        ///     
        /// </remarks>
        /// <response code="200">Success, Chamada realizada com sucesso</response>
        /// <response code="400">Bad Request, chamada inválida</response>  
        [ProducesResponseType(typeof(Result), 200)]
        [ProducesResponseType(typeof(ExceptionPayload), 400)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new RobotDeleteCommand(id));

            return HandleCommand(result);
        }
        #endregion
        
    }
}
