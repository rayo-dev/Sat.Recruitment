using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Handlers.CommandHandlers;

namespace Sat.Recruitment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CreateUserHandler).GetTypeInfo().Assembly);

            return services;
        }
    }
}
