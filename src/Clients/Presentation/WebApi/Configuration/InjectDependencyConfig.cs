using Application.AppServices;
using Domain.Interfaces.AppService;
using Domain.Interfaces.Repository;
using Domain.Interfaces;
using Domain.Notifications;
using Repository.Repositories;
using Repository.Contexts;

namespace WebApi.Configuration
{
    public static class InjectDependencyConfig
    {
        public static void InjectDependencyRegister(this IServiceCollection services)
        {
            services.AddScoped<ClientsContext>();
            services.AddScoped<INotificationContext, NotificationContext>();


            #region AppServices

            services.AddScoped<IAuthAppService, AuthAppService>();
            services.AddScoped<IClienteAppService, ClienteAppService>();

            #endregion

            #region Repositories

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            #endregion
        }
    }
}
