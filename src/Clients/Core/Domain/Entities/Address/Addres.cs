using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Addres : IEntityBase
    {
        public int Id { get; set; }
        public string Logradouro { get; private set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int ClientId { get; private set; }
        public virtual Cliente Client { get; set; }

        /// <summary>
        /// Create Address
        /// </summary>
        /// <param name="logradouro"></param>
        /// <param name="clientId"></param>
        public Addres(string logradouro, int clientId)
        {
            ClientId = clientId;
            Logradouro = logradouro;
        }

        /// <summary>
        /// Update Address
        /// </summary>
        /// <param name="logradouro"></param>
        public void Update(string logradouro)
        {
            Logradouro = logradouro;
        }
    }
}
