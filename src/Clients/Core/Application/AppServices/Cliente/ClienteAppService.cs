using Domain.DTOs;
using Domain.Utils;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.AppService;
using Domain.Interfaces.Repository;
using Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly INotificationContext _notification;
        private readonly IClienteRepository _repository;

        public ClienteAppService(INotificationContext notification,
                                 IClienteRepository repository)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<Tuple<string>> AddAsync(ClienteDto cliente)
        {
            if (string.IsNullOrEmpty(cliente.Name))
                _notification.AddNotification(new Notification("Nome deve ser informado"));

            if (string.IsNullOrEmpty(cliente.Email))
                _notification.AddNotification(new Notification("E-mail deve ser informado"));

            if (_repository.ValidateByEmail(cliente.Email))
                _notification.AddNotification(new Notification("E-mail já cadastrado"));

            if (!_notification.HasNotifications)
            {
                var senhaHash = cliente.Email.GenerateHashPassword($"{cliente.Name}{DateTime.Now.Year}");

                var userDomain = new User(cliente.Email, senhaHash);
                var clienteDomain = new Cliente(cliente.Name, cliente.Email,cliente.Logotipo);

                userDomain.Cliente = clienteDomain;

                //if (clienteDomain.Invalid)
                //    _notification.AddNotification(clienteDomain.ValidationResult);
                //else
                //{
                    await _repository.AddAsync(clienteDomain);
                    await _repository.UnitOfWork.Commit();
                //}
            }

            return new Tuple<string>("Cadastro realizado com sucesso, utilize seu email como login e seu nome+anoatual parasenha");
        }

        public async Task Delete(int userId, int clienteId)
        {
            var clienteDomain = _repository.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            if (_notification.HasNotifications)
                return;

            _repository.Delete(clienteDomain);
            await _repository.UnitOfWork.Commit();
        }

        public ClienteResponseDto GetById(int userId, int clienteId)
        {
            var clienteDomain = _repository.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            return clienteDomain != null ? new ClienteResponseDto
            {
                Id = clienteDomain.Id,
                Email = clienteDomain.Email,
                Logotipo = clienteDomain.Logotipo,
                Name = clienteDomain.Name,
                Logradouros = clienteDomain.Logradouros.Select(x=> new AddressResponseDto
                {
                    Id=x.Id,
                    Logradouro = x.Logradouro
                }).ToList()
            } : null;
        }

        public ClienteResponseDto GetByUserId(int userId)
        {
            var clienteDomain = _repository.GetByUserId(userId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            return clienteDomain != null ? new ClienteResponseDto
            {
                Id = clienteDomain.Id,
                Email = clienteDomain.Email,
                Logotipo = clienteDomain.Logotipo,
                Name = clienteDomain.Name,
                Logradouros = clienteDomain.Logradouros.Select(x => new AddressResponseDto
                {
                    Id = x.Id,
                    Logradouro = x.Logradouro
                }).ToList()
            } : null;
        }

        public async Task Update(int userId, int clienteId, ClienteDto cliente)
        {
            var clienteDomain = _repository.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            if (string.IsNullOrEmpty(cliente.Name))
                _notification.AddNotification(new Notification("Nome deve ser informado"));

            if (string.IsNullOrEmpty(cliente.Email))
                _notification.AddNotification(new Notification("E-mail deve ser informado"));

            if (_repository.ValidateByEmail(cliente.Email))
                _notification.AddNotification(new Notification("E-mail já cadastrado"));

            if (!_notification.HasNotifications)
            {
                clienteDomain.Update(cliente.Name, cliente.Logotipo);

                //if (clienteDomain.Invalid)
                //    _notification.AddNotification(clienteDomain.ValidationResult);
                //else
                //{
                await _repository.AddAsync(clienteDomain);
                await _repository.UnitOfWork.Commit();
                //}
            }
        }
    }
}
