using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Logotipo { get; private set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int UserId { get; private set; }
        public virtual User User { get; set; }

        public virtual ICollection<Addres> Logradouros { get; set; }

        public Client()
        {
            Logradouros = new HashSet<Addres>();
        }

        /// <summary>
        /// Create Cliente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="logotipo"></param>
        public Client(string name, string email, string logotipo)
        {
            Name = name;
            Email = email;
            Logotipo = logotipo;
        }

        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="name"></param>
        /// <param name="logotipo"></param>
        public void Update(string name, string logotipo)
        {
            Name = name;
            Logotipo = logotipo;
        }

        public void AssociarUser(int userId)
        {
            UserId = userId;
        }
    }
}
