using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Cqrs.Dependencies
{
    public interface IHandlerResolver
    {
        THandler ResolveHandler<THandler>();
    }
}
