using Domain.DTOs;
using Domain.Interfaces;
using Domain.Interfaces.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Address
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class AddressController : MainController
    {
        private readonly IAddressAppService _service;

        public AddressController(IAddressAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adicionar um novo logradouro ao cliente
        /// </summary>
        /// <param name="address"></param>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("logradouro/{clienteId}")]
        public async Task<IActionResult> AddLogradouro(int clienteId, [FromBody] AddressDto address)
        {
            return Ok(await _service.AddAsync(UserLoggedId, clienteId, address));
        }

        /// <summary>
        /// Buscar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("logradouro/{addressId}")]
        public IActionResult GetLogradouroById(int addressId)
        {
            return Ok(_service.GetById(UserLoggedId, addressId));
        }

        /// <summary>
        /// Buscar um logradouros de um cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("logradouros/{clienteId}")]
        public IActionResult GetLogradourosByClienteId(int clienteId)
        {
            return Ok(_service.GetByClienteId(UserLoggedId, clienteId));
        }

        /// <summary>
        /// Deletar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("logradouro/{addressId}")]
        public async Task<IActionResult> DeleteLogradouro(int addressId)
        {
            await _service.Delete(UserLoggedId, addressId);
            return Ok(new Tuple<string>("Logradouro deletado com sucesso"));
        }

        /// <summary>
        /// Atualizar um logradouro de um cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="addressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("logradouro/{clienteId}/{addressId}")]
        public async Task<IActionResult> UpdateLogradouro(int clienteId, int addressId, [FromBody] AddressDto address)
        {
            await _service.Update(UserLoggedId, clienteId, addressId, address);
            return Ok(new Tuple<string>("Logradouro atualizado com sucesso"));
        }
    }
}