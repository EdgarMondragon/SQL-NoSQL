using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Configuration
{
	public interface IMongoClientConfig
	{
		string ConnectionString { get; }

		string Database { get; }

		string Collection { get; }

	}
}
