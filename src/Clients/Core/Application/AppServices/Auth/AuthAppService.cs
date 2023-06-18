﻿using Domain.Auth;
using Domain.DTOs;
using Domain.Utils;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.AppService;
using Domain.Interfaces.Repository;
using Domain.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class AuthAppService : IAuthAppService
    {
        private readonly INotificationContext _notification;
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly TokenConfiguration _tokenConfigurations;

        public AuthAppService(INotificationContext notification,
                                 IAuthRepository repository,
                                 IConfiguration config)
        {
            _config = config;
            _repository = repository;
            _notification = notification;
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["KeyApp"]));

            _tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(config
                            .GetSection("TokenConfigurations"))
                            .Configure(_tokenConfigurations);
        }
        public async Task<AuthResponseDto> AutenticateAsync(AuthDto auth)
        {
            if (string.IsNullOrEmpty(auth.Login))
                _notification.AddNotification(new Notification("Login is required"));

            if (string.IsNullOrEmpty(auth.Password))
                _notification.AddNotification(new Notification("Password is required"));

            if (!_notification.HasNotifications)
            {
                var senhaHash = auth.Login.GenerateHashPassword(auth.Password);
                var user = await _repository.AutenticateAsync(auth.Login, senhaHash);

                if (user == null)
                {
                    _notification.AddNotification(new Notification("Invalid login or password"));
                    return null;
                }

                return GenerateToken(user);
            }
            else
                return null;
        }

        public async Task UpdatePassword(int userId, UserDto user)
        {
           var userDomain  =_repository.GetUserById(userId);

            if (userDomain == null)
            {
                _notification.AddNotification(new Notification("User not found"));
            }
            else
            {
                var senhaHash = user.Login.GenerateHashPassword(user.NewPassword);

                userDomain.UpdatePassword(senhaHash);

                _repository.UpdatePassword(userDomain);
                await _repository.UnitOfWork.Commit();
            }
        }

        private AuthResponseDto GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
            };

            var dateExpiration = DateTime.UtcNow.AddMinutes(_tokenConfigurations.Seconds);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Expires = dateExpiration,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.Cliente.Name,
                ExpireIn = _tokenConfigurations.Seconds
            };
        }
    }
}
