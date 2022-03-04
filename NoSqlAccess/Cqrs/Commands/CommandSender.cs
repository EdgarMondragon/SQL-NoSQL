using DotNetStarter.Abstractions;
using NoSqlAccess.Cqrs.Dependencies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Cqrs.Commands
{
    public class CommandSender : ICommandSender
    {
        private readonly IHandlerResolver handlerResolver;

        public CommandSender(
            IHandlerResolver handlerResolver)
        {
            this.handlerResolver = handlerResolver;
        }

        /// <inheritdoc />
        public Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = handlerResolver.ResolveHandler<ICommandHandlerAsync<TCommand>>();

            return handler.HandleAsync(command);
        }

        /// <summary>
        /// Sends the specified command.
        /// The command handler must implement Api.Cqrs.Commands.ICommandHandler.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <inheritdoc />
        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = handlerResolver.ResolveHandler<ICommandHandler<TCommand>>();

            handler.Handle(command);
        }
    }
}
