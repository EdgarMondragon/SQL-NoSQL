using DotNetStarter.Abstractions;
using Entities.NoSQLEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Configuration
{
	public class DomainDbContext : IDomainDbContext
	{
		private readonly IMongoDatabase database;
		private readonly string collection;

		public DomainDbContext(IMongoClientConfig config)
		{
			var mongoUrl = new MongoUrl(config.ConnectionString);
			var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);
			var client = new MongoClient(mongoClientSettings);

			database = client.GetDatabase(config.Database);
			collection = config.Collection;
		}

		public IMongoCollection<Client> GetClients()
		{
			return database.GetCollection<Client>(collection);
		}
	}
}
