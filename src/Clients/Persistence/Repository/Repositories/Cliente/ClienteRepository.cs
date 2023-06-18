using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ClienteRepository(ClientsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente) =>
            await _context.Cliente.AddAsync(cliente);

        public void Update(Cliente cliente) =>
            _context.Cliente.Update(cliente);

        public void Delete(Cliente cliente) =>
            _context.Cliente.Remove(cliente);

        public Cliente GetByUserId(int userId) =>
            _context.Cliente
                .Include(x => x.Users)
                .Include(x => x.Logradouros)
                .FirstOrDefault(x => x.Users.Any(u => u.Id == userId));

        public Cliente GetById(int clienteId) =>
            _context.Cliente
                .Include(x => x.Users)
                .Include(x => x.Logradouros)
                .FirstOrDefault(x => x.Id == clienteId);

        public bool ValidateByEmail(string email) =>
            _context.Cliente.Any(x => x.Email == email);

        public bool ValidateByEmailUpdate(int clienteId, string email) =>
            _context.Cliente.Any(x => x.Email == email && x.Id != clienteId);
    }
}
