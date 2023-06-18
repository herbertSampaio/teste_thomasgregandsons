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
    public class Cliente : Validation, IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Logotipo { get; private set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Addres> Logradouros { get; set; }

        public Cliente()
        {
            Users = new List<User>();
            Logradouros = new HashSet<Addres>();
        }

        /// <summary>
        /// Criar Cliente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="logotipo"></param>
        public Cliente(string name, string email, string logotipo)
        {
            Name = name;
            Email = email;
            Logotipo = logotipo;

            Users = new List<User>();
            Logradouros = new HashSet<Addres>();
        }

        /// <summary>
        /// Atualizar Cliente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="logotipo"></param>
        public void Update(string name, string logotipo)
        {
            Name = name;
            Logotipo = logotipo;
        }

        /// <summary>
        /// Metodo para validação dos campos
        /// </summary>
        public void Validate()
        {
            Validate(this, new ClienteValidation());
        }
    }

    /// <summary>
    /// Validação dos dados do cliente
    /// </summary>
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome deve conter no maximo 100 caracters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome deve conter no maximo 100 caracters");

            RuleFor(x => x.Logotipo)
                .NotEmpty()
                .WithMessage("Logotipo é obrigatório")
                .MaximumLength(150)
                .WithMessage("Nome deve conter no maximo 100 caracters");

            RuleFor(x => x.Logradouros)
                .NotEmpty()
                .WithMessage("É obrigatório pelo menos um logradouro");

            RuleForEach(x => x.Logradouros).ChildRules(order =>
            {
                order.RuleFor(l => l.Logradouro)
                .NotEmpty()
                .WithMessage("Logradouro é obrigatório")
                .MaximumLength(100)
                .WithMessage("Logradouro deve conter no maximo 100 caracters");
            });
        }
    }
}
