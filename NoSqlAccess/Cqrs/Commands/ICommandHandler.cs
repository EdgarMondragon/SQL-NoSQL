using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand>
       where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
