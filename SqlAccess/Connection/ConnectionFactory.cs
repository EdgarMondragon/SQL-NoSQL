using DotNetStarter.Abstractions;
using GodSharp.Data.Common.DbProvider;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlAccess.Connection
{
	[Registration(typeof(IConnectionFactory), Lifecycle.Scoped)]
	public class ConnectionFactory : IConnectionFactory
	{
		private readonly IConfiguration connectionString;
		private IDbConnection Connection;

		public ConnectionFactory(IConfiguration config)
		{
			connectionString = config;
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}

		public IDbConnection GetConnection
		{
			get
			{
				return new SqlConnection(connectionString.GetConnectionString("ConnectionString"));
			}
		}

		private bool disposedValue = false;
	}
}
