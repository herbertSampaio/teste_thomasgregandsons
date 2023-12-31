﻿using Domain.DTOs;
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
            if (!string.IsNullOrEmpty(cliente.Email) && _repository.ValidateByEmail(cliente.Email))
                _notification.AddNotification(new Notification("E-mail já cadastrado"));

            var senhaGuid = Guid.NewGuid().ToString().ToUpper().Split('-')[0];
            if (!_notification.HasNotifications)
            {
                var senhaHash = cliente.Email.GenerateHashPassword(senhaGuid);

                var userDomain = new User(cliente.Email, senhaHash);
                var clienteDomain = new Cliente(cliente.Name, cliente.Email, cliente.Logotipo);

                clienteDomain.Users.Add(userDomain);

                foreach (var item in cliente.Logradouros)
                    clienteDomain.Logradouros.Add(new Addres(item.Logradouro, clienteDomain.Id));

                clienteDomain.Validate();

                if (clienteDomain.Invalid)
                    _notification.AddNotification(clienteDomain.ValidationResult);
                else
                {
                    await _repository.AddAsync(clienteDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }

            return new Tuple<string>($"Cadastro realizado com sucesso, utilize seu email como login e o codigo {senhaGuid} como senha");
        }

        public async Task Delete(int userId, int clienteId)
        {
            var clienteDomain = _repository.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            if (clienteDomain != null && !clienteDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

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

            if (clienteDomain != null && !clienteDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return null;
            else
                return new ClienteResponseDto
                {
                    Id = clienteDomain.Id,
                    Email = clienteDomain.Email,
                    Logotipo = clienteDomain.Logotipo,
                    Name = clienteDomain.Name,
                    Logradouros = clienteDomain.Logradouros.Select(x => new AddressResponseDto
                    {
                        Id = x.Id,
                        ClienteId = x.ClienteId,
                        Logradouro = x.Logradouro
                    }).ToList()
                };
        }

        public async Task Update(int userId, int clienteId, ClienteDto cliente)
        {
            var clienteDomain = _repository.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            if (clienteDomain != null && !clienteDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (!string.IsNullOrEmpty(cliente.Email) && _repository.ValidateByEmailUpdate(clienteId, cliente.Email))
                _notification.AddNotification(new Notification("E-mail já cadastrado"));

            if (!_notification.HasNotifications)
            {
                clienteDomain.Logradouros.Clear();
                clienteDomain.Update(cliente.Name, cliente.Logotipo);

                foreach (var item in cliente.Logradouros)
                    clienteDomain.Logradouros.Add(new Addres(item.Logradouro, clienteDomain.Id));

                clienteDomain.Validate();

                if (clienteDomain.Invalid)
                    _notification.AddNotification(clienteDomain.ValidationResult);
                else
                {
                    _repository.Update(clienteDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }
        }
    }
}
