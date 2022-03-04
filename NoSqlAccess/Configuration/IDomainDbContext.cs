using Entities.NoSQLEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSqlAccess.Configuration
{
	public interface IDomainDbContext
	{
		IMongoCollection<Client> GetClients();
	}
}
