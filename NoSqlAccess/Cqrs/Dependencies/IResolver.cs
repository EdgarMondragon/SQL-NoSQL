using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Cqrs.Dependencies
{
    public interface IResolver
    {
        T Resolve<T>();

        IEnumerable<T> ResolveAll<T>();
    }
}
