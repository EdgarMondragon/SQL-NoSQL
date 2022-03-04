using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SqlAccess.Connection
{
	public interface IConnectionFactory : IDisposable
	{
		IDbConnection GetConnection { get; }
	}
}
