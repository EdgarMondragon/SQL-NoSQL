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
	public class GetClientListHandler : IQueryHandlerAsync<GetClientsQuery, List<GetClientCommand>>
	{
		private readonly IDomainDbContext dbContext;

		public GetClientListHandler(IDomainDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<List<GetClientCommand>> RetrieveAsync(GetClientsQuery query)
		{
			List<GetClientCommand> clientCommands = new List<GetClientCommand>();

			try
			{
				var clientList = await dbContext.GetClients().AsQueryable().ToListAsync();

				foreach (var item in clientList)
				{
					clientCommands.Add(new GetClientCommand
					{
						Age = item.Age,
						Id = item.Id.ToString(),
						LastName = item.LastName,
						Name = item.Name,
						State = item.State
					});
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return clientCommands;
		}
	}
}
