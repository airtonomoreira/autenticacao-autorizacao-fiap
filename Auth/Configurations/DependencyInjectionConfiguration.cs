using Auth.Domain.User.Repository;
using Auth.Domain.User.Services;
using Auth.Domain.User.Token;
using Auth.Domain.User.Token.Services;
using Auth.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
