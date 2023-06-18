using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public User()
        {
        }

        /// <summary>
        /// Criar Usuario
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public User(string login, string password)
        {
            Ativo = true;
            Login = login;
            Password = password;
        }

        /// <summary>
        /// Atualizar senha
        /// </summary>
        /// <param name="password"></param>
        public void UpdatePassword(string password)
        {
            this.Password = password;
        }

        /// <summary>
        /// Atualizar status
        /// </summary>
        /// <param name="ativo"></param>
        public void UpdateStatus(bool ativo)
        {
            Ativo = ativo;
        }

        /// <summary>
        /// Associar Cliente
        /// </summary>
        /// <param name="clienteId"></param>
        public void AssociarCliente(int clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
