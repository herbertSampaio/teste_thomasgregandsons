using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ClientsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public AuthRepository(ClientsContext context)
        {
            _context = context;
        }

        public async Task<User> AutenticateAsync(string login, string password) =>
            await _context.User
                .Include(x=>x.Cliente)
                .FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower() && x.Password == password);

        public User GetUserById(int userId) =>
            _context.User.Find(userId);

        public void UpdatePassword(User user) =>
            _context.User.Update(user);
    }
}
