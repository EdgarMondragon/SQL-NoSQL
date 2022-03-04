using DotNetStarter.Abstractions;
using Entities.NoSQLEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSqlAccess.Configuration;
using NoSqlAccess.Cqrs.Queries;
using NoSqlAccess.Domain.Commands.Command;
using NoSqlAccess.Domain.Queries.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Domain.Queries.Handler
{
	public class GetClientHandler : IQueryHandlerAsync<GetClientsQuery, GetClientCommand>
	{
		private readonly IDomainDbContext dbContext;

		public GetClientHandler(IDomainDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<GetClientCommand> RetrieveAsync(GetClientsQuery query)
		{
			GetClientCommand clientCommand = new GetClientCommand();

			try
			{
				var filter = new FilterDefinitionBuilder<Client>().Eq(x => x.Id, ObjectId.Parse(query.Id));
				var client = await dbContext.GetClients().Find(filter).FirstOrDefaultAsync();

				clientCommand.Id = client.Id.ToString();
				clientCommand.Name = client.Name;
				clientCommand.LastName = client.LastName;
				clientCommand.State = client.State;
				clientCommand.Age = client.Age;


			}
			catch (Exception ex)
			{
				throw ex;
			}
			return clientCommand;
		}
	}
}
