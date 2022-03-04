using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Cqrs.Exceptions
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(Type handlerType)
            : base(BuildErrorMessage(handlerType))
        {
        }

        private static string BuildErrorMessage(Type handlerType)
        {
            return $"No handler found that implements '{handlerType.FullName}'";
        }
    }
}
