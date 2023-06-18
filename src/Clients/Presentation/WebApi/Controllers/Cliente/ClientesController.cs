using Domain.DTOs;
using Domain.Interfaces.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Cliente
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class ClientesController : MainController
    {
        private readonly IClienteAppService _service;

        public ClientesController(IClienteAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adicionar um novo cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cliente")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCliente([FromBody] ClienteDto cliente)
        {
            return Ok(await _service.AddAsync(cliente));
        }

        /// <summary>
        /// Buscar um cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cliente/{clienteId}")]
        public IActionResult GetClienteById(int clienteId)
        {
            return Ok(_service.GetById(UserLoggedId, clienteId));
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("cliente/{clienteId}")]
        public async Task<IActionResult> DeleteCategory(int clienteId)
        {
            await _service.Delete(UserLoggedId, clienteId);
            return Ok(new Tuple<string>("Cliente deletado com sucesso"));
        }

        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("cliente/{clienteId}")]
        public async Task<IActionResult> UpdateCliente(int clienteId, [FromBody] ClienteDto cliente)
        {
            await _service.Update(UserLoggedId, clienteId, cliente);
            return Ok(new Tuple<string>("Cliente atualizado com sucesso"));
        }
    }
}