using Microsoft.AspNetCore.Mvc;
using NoSqlAccess.Cqrs;
using NoSqlAccess.Domain.Commands.Command;
using NoSqlAccess.Domain.Queries.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSQLNoSQL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientNoSQLController : ControllerBase
	{
		private readonly IDispatcher dispatcher;

		public ClientNoSQLController(IDispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
		}

		// GET: api/<ClientNoSQLController>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var clients = await dispatcher.GetResultAsync<GetClientsQuery, List<GetClientCommand>>(new GetClientsQuery());

			return Ok(clients);
		}

		// GET api/<ClientNoSQLController>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var client = await dispatcher.GetResultAsync<GetClientsQuery, GetClientCommand>(new GetClientsQuery
			{
				Id = id
			});

			return Ok(client);
		}

		// POST api/<ClientNoSQLController>
		[HttpPost]
		public async Task PostAsync([FromBody] PostClientCommand command)
		{
			await dispatcher.SendAsync(command);
		}

		// PUT api/<ClientNoSQLController>/5
		[HttpPut("{id}")]
		public async Task PutAsync(string id, [FromBody] PutClientCommand command)
		{
			command.Id = id;
			await dispatcher.SendAsync(command);
		}

		// DELETE api/<ClientNoSQLController>/5
		[HttpDelete("{id}")]
		public async Task DeleteAsync(string id)
		{
			var client = await dispatcher.GetResultAsync<GetClientsQuery, GetClientCommand>(new GetClientsQuery
			{
				Id = id
			});

			if (client != null)
			{
				await dispatcher.SendAsync(new DeleteClientCommand
				{
					Id = client.Id
				});
			}
		}
	}
}
