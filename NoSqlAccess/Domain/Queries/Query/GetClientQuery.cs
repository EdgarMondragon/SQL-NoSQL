using NoSqlAccess.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Domain.Queries.Query
{
	public class GetClientQuery : IQuery
	{
		public string Id { get; set; }
		//public string Name { get; set; }
		//public string LastName { get; set; }
		//public int Age { get; set; }
		//public string State { get; set; }
	}
}
