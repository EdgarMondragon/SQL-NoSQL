using DotNetStarter.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Configuration
{
	public class MongoClientConfig : IMongoClientConfig
	{
		private readonly IConfiguration configuration;

		public MongoClientConfig(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public string ConnectionString => "";

		public string Database => "demodb";

		public string Collection => "clients";
	}
}
