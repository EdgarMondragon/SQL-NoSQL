using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Cqrs.Commands
{
    public interface ICommandHandlerAsync<in TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
