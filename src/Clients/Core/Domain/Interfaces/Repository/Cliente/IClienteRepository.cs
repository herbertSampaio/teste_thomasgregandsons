using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IClienteRepository : IRepository
    {
        Task AddAsync(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Cliente cliente);
        Cliente GetByUserId(int userId);
        Cliente GetById(int clienteId);
        bool ValidateByEmail(string email);
        bool ValidateByEmailUpdate(int clienteId, string email);
    }
}
