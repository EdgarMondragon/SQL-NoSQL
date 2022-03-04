using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery, out TResult>
       where TQuery : IQuery
    {
        TResult Retrieve(TQuery query);
    }
}
