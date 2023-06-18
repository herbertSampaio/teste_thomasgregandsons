using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.AppService
{
    public interface IClienteAppService
    {
        Task<Tuple<string>> AddAsync(ClienteDto cliente);
        Task Update(int userId, int clienteId, ClienteDto cliente);
        Task Delete(int userId, int clienteId);
        ClienteResponseDto GetById(int userId, int clienteId);
        ClienteResponseDto GetByUserId(int userId);
    }
}
