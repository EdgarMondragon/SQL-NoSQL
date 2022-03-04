using DotNetStarter.Abstractions;
using NoSqlAccess.Cqrs.Dependencies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Cqrs.Queries
{
    [Registration(typeof(IQueryProcessor), Lifecycle.Scoped)]
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IHandlerResolver handlerResolver;

        public QueryProcessor(IHandlerResolver handlerResolver)
        {
            this.handlerResolver = handlerResolver;
        }

        /// <inheritdoc />
        public Task<TResult> ProcessAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = handlerResolver.ResolveHandler<IQueryHandlerAsync<TQuery, TResult>>();

            return handler.RetrieveAsync(query);
        }

        /// <inheritdoc />
        public TResult Process<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = handlerResolver.ResolveHandler<IQueryHandler<TQuery, TResult>>();

            return handler.Retrieve(query);
        }
    }
}
