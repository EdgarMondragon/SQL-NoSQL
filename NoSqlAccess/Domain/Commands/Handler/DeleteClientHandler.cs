using DotNetStarter.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSqlAccess.Configuration;
using NoSqlAccess.Cqrs.Commands;
using NoSqlAccess.Domain.Commands.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.NoSQLEntities.Domain.Commands.Handler
{
	public class DeleteClientHandler : ICommandHandlerAsync<DeleteClientCommand>
	{
		private readonly IDomainDbContext dbContext;

		public DeleteClientHandler(IDomainDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task HandleAsync(DeleteClientCommand command)
		{
			try
			{
				var filter = Builders<Client>.Filter.Eq(x => x.Id, ObjectId.Parse(command.Id));
				await dbContext.GetClients().DeleteOneAsync(filter);

			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}
	}
}
