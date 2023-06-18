using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IAddressRepository : IRepository
    {
        Task AddAsync(Addres address);
        void Update(Addres address);
        void Delete(Addres address);
        Addres GetById(int addressId);
        Task<List<Addres>> GetByClienteId(int clienteId);
    }
}
