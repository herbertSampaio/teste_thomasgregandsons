using Domain.Interfaces;
using Domain.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Addres : Validation, IEntityBase
    {
        public int Id { get; set; }
        public string Logradouro { get; private set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int ClienteId { get; private set; }
        public virtual Cliente Cliente { get; set; }

        public Addres()
        {
            
        }

        /// <summary>
        /// Criar Logradouro
        /// </summary>
        /// <param name="logradouro"></param>
        /// <param name="clientId"></param>
        public Addres(string logradouro, int clientId)
        {
            ClienteId = clientId;
            Logradouro = logradouro;
        }

        /// <summary>
        /// Atualizar Logradouro
        /// </summary>
        /// <param name="logradouro"></param>
        public void Update(string logradouro)
        {
            Logradouro = logradouro;
        }
    }

    /// <summary>
    /// Validação dos dados do logradouro
    /// </summary>
    public class AddressValidation : AbstractValidator<Addres>
    {
        public AddressValidation()
        {
            RuleFor(x => x.Logradouro)
                .NotEmpty()
                .WithMessage("Logradouro é obrigatório")
                .MaximumLength(100)
                .WithMessage("Logradouro deve conter no maximo 100 caracters");
        }
    }
}
