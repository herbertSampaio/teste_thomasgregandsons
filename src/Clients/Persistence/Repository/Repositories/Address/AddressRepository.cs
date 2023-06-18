using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ClientsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public AddressRepository(ClientsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Addres address) =>
            await _context.Address.AddAsync(address);

        public void Update(Addres address) =>
            _context.Address.Update(address);

        public void Delete(Addres address) =>
            _context.Address.Remove(address);

        public Addres GetById(int addressId) =>
            _context.Address
                .Include(x => x.Cliente).ThenInclude(u => u.Users)
                .FirstOrDefault(x => x.Id == addressId);

        public List<Addres> GetByClienteId(int clienteId) =>
            _context.Address
                .Where(x => x.ClienteId == clienteId).ToList();
    }
}
