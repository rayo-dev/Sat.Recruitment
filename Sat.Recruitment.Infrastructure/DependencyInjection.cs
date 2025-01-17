﻿using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Infrastructure.Files;
using Sat.Recruitment.Infrastructure.Repositories;

namespace Sat.Recruitment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserFile, UserFile>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
