﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Stardust.Paradox.Data
{
    public static class DependencyResolverAdapter
    {
        public static IServiceCollection AddParadox<Tcontext>(this IServiceCollection services, Func<IServiceProvider,IGremlinLanguageConnector> createDatabaseProvider) where Tcontext: class, IGraphContext
        {
            
            services.AddEntityBinding((entity, implementation) => services.AddTransient(entity, implementation))
                .AddScoped<Tcontext, Tcontext>()
                .AddScoped(createDatabaseProvider);

            return services;
        }

        private static Action<Type, Type> _addToCollection;

        public static T AddEntityBinding<T>(this T serviceCollection, Action<Type, Type> addToCollection)
        {
            _addToCollection = addToCollection;
            return serviceCollection;
        }

        internal static void AddEntity(Type entityType, Type entityImplementation)
        {
            _addToCollection(entityType, entityImplementation);
        }

        internal static T GetService<T>(this IServiceProvider provider)
        {
            var r = provider.GetService(typeof(T));
            if (r == null) return default(T);
            return (T)r;
        }
    }
}
