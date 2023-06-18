using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class AddressAppService: IAddressAppService
    {
        private readonly INotificationContext _notification;
        private readonly IAddressRepository _repository;
        private readonly IClienteRepository _repositoryCliente;

        public AddressAppService(INotificationContext notification,
                                 IAddressRepository repository,
                                 IClienteRepository repositoryCliente)
        {
            _repository = repository;
            _notification = notification;
            _repositoryCliente = repositoryCliente;
        }

        public async Task<Tuple<string>> AddAsync(int userId, int clienteId, AddressDto address)
        {
            var clienteDomain = _repositoryCliente.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente informado não existe"));

            if (clienteDomain != null && !clienteDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (!_notification.HasNotifications)
            {
                var addressDomain = new Addres(address.Logradouro, clienteId);

                addressDomain.Validate();

                if (addressDomain.Invalid)
                    _notification.AddNotification(addressDomain.ValidationResult);
                else
                {
                    await _repository.AddAsync(addressDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }

            return new Tuple<string>($"Logradouro adicionado ao cliente {clienteDomain.Name} com sucesso");
        }

        public async Task Delete(int userId, int addressId)
        {
            var addressDomain = _repository.GetById(addressId);

            if (addressDomain == null)
                _notification.AddNotification(new Notification("Logradouro não existe"));

            if (addressDomain != null && !addressDomain.Cliente.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return;

            _repository.Delete(addressDomain);
            await _repository.UnitOfWork.Commit();
        }

        public AddressResponseDto GetById(int userId, int addressId)
        {
            var addressDomain = _repository.GetById(addressId);

            if (addressDomain == null)
                _notification.AddNotification(new Notification("Logradouro não existe"));

            if (addressDomain != null && !addressDomain.Cliente.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return null;
            else
                return new AddressResponseDto
                {
                    Id = addressDomain.Id,
                    ClienteId = addressDomain.ClienteId,
                    Logradouro = addressDomain.Logradouro,
                };
        }

        public List<AddressResponseDto> GetByClienteId(int userId, int clienteId)
        {
            var clienteDomain = _repositoryCliente.GetById(clienteId);

            if (clienteDomain == null)
                _notification.AddNotification(new Notification("Cliente informado não existe"));

            if (clienteDomain != null && !clienteDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            var listAddress = _repository.GetByClienteId(clienteId);

            if (_notification.HasNotifications)
                return null;
            else
                return listAddress.Select(x=> new AddressResponseDto
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    Logradouro = x.Logradouro,
                }).ToList();
        }

        public async Task Update(int userId, int clienteId, int addressId, AddressDto address)
        {
            var addressDomain = _repository.GetById(addressId);

            if (addressDomain == null)
                _notification.AddNotification(new Notification("Logradouro não existe"));

            if (addressDomain != null && !addressDomain.Cliente.Users.All(x => x.Id == userId) && addressDomain.ClienteId != clienteId)
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (!_notification.HasNotifications)
            {
                addressDomain.Update(address.Logradouro);

                addressDomain.Validate();

                if (addressDomain.Invalid)
                    _notification.AddNotification(addressDomain.ValidationResult);
                else
                {
                    _repository.Update(addressDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }
        }
    }
}