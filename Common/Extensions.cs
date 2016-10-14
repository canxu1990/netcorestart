using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddWkMvcDI(this IServiceCollection services)
        {
            return services;
        }
        public static IApplicationBuilder UserWkMvcDI(this IApplicationBuilder builder)
        {
            DI.ServiceProvider = builder.ApplicationServices;
            return builder;
        }
    }

    public static class DI
    {
        public static IServiceProvider ServiceProvider
        {
            get;set;
        }
    }
}
