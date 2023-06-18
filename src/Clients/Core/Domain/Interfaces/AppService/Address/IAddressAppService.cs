using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressAppService
    {
        Task<Tuple<string>> AddAsync(int userId, int clienteId, AddressDto address);
        Task Update(int userId, int clienteId, int addressId, AddressDto address);
        Task Delete(int userId, int addressId);
        AddressResponseDto GetById(int userId, int addressId);
        List<AddressResponseDto> GetByClienteId(int userId, int clienteId);
    }
}
