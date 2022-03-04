using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Cqrs.Queries
{
    public interface IQueryHandlerAsync<in TQuery, TResult>
      where TQuery : IQuery
    {
        Task<TResult> RetrieveAsync(TQuery query);
    }
}
