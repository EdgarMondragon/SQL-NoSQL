using DotNetStarter.Abstractions;
using Entities.NoSQLEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSqlAccess.Configuration;
using NoSqlAccess.Cqrs.Commands;
using NoSqlAccess.Domain.Commands.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlAccess.Domain.Commands.Handler
{
	public class PostClientHandler : ICommandHandlerAsync<PostClientCommand>
	{
		private readonly IDomainDbContext dbContext;

		public PostClientHandler(IDomainDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task HandleAsync(PostClientCommand command)
		{
			try
			{
				var client = new Client
				{
					Age = command.Age,
					LastName = command.LastName,
					Name = command.Name,
					State = command.State
				};
				ObjectId objectId = new ObjectId();
				if (!string.IsNullOrEmpty(command.Id) && ObjectId.TryParse(command.Id, out objectId))
				{
					var filter = Builders<Client>.Filter.Eq(x => x.Id, ObjectId.Parse(command.Id));

					var clientFind = await dbContext.GetClients().Find(filter).FirstOrDefaultAsync();
					if (clientFind != null)
					{
						client.Id = clientFind.Id;
						await dbContext.GetClients().ReplaceOneAsync(filter, client);
					}
					else
					{
						await dbContext.GetClients().InsertOneAsync(client);
					}
				}
				else
				{
					await dbContext.GetClients().InsertOneAsync(client);
				}




			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
