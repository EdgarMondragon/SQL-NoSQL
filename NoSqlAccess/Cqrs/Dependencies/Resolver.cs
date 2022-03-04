using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NoSqlAccess.Cqrs.Dependencies
{
    //[Registration(typeof(IResolver), Lifecycle.Scoped)]
    public class Resolver : IResolver
    {
        private readonly IServiceProvider serviceProvider;

        public Resolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return serviceProvider.GetServices<T>();
        }
    }
}
