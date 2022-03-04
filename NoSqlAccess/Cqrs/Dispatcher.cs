using DotNetStarter.Abstractions;
using NoSqlAccess.Cqrs.Commands;
using NoSqlAccess.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Cqrs
{
    [Registration(typeof(IDispatcher), Lifecycle.Scoped)]
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandSender commandSender;
        private readonly IQueryProcessor queryProcessor;

        public Dispatcher(
            ICommandSender commandSender,
            IQueryProcessor queryProcessor)
        {
            this.commandSender = commandSender;
            this.queryProcessor = queryProcessor;
        }

        /// <inheritdoc />
        public Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            return commandSender.SendAsync(command);
        }

        /// <inheritdoc />
        public Task<TResult> GetResultAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
        {
            return queryProcessor.ProcessAsync<TQuery, TResult>(query);
        }

        /// <inheritdoc />
        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            commandSender.Send(command);
        }

        /// <inheritdoc />
        public TResult GetResult<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
        {
            return queryProcessor.Process<TQuery, TResult>(query);
        }
    }
}
