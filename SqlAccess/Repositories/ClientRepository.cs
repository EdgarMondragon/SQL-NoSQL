using DotNetStarter.Abstractions;
using Entities.SQLEntities;
using SqlAccess.Base;
using SqlAccess.Connection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlAccess.Repositories
{
	[Registration(typeof(IClientRepository), Lifecycle.Scoped)]
	public class ClientRepository : SQLBaseRepository<Client>, IClientRepository
	{
		IConnectionFactory _connectionFactory;
		public ClientRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}
	}
}
