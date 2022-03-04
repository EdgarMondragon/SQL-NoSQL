using AutoMapper;
using Entities.SQLEntities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using SqlAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSQLNoSQL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientSQLController : ControllerBase
	{
		public readonly IClientRepository clientRepository;

		public ClientSQLController(IClientRepository clientRepository)
		{
			this.clientRepository = clientRepository;
		}

		// GET: api/<ClientSQLController>
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var clients = await clientRepository.GetAllAsync();
			if (!clients.Any())
			{
				return new NoContentResult();
			}

			return new ObjectResult(clients);
		}

		// GET api/<ClientSQLController>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var client = await clientRepository.GetAsync(id);
			return new ObjectResult(client);
		}

		// POST api/<ClientSQLController>
		[HttpPost]
		public async Task<ClientModel> PostAsync([FromBody] ClientModel client)
		{
			var model = Mapper.Map<Client>(client);

			var result =Mapper.Map<ClientModel>(await clientRepository.InsertAsync(model));

			return result;
		}

		// PUT api/<ClientSQLController>/5
		[HttpPut("{id}")]
		public async Task<bool> PutAsync(int id, [FromBody] ClientModel client)
		{
			var model = Mapper.Map<Client>(client);
			model.Id = id;
			var result = await clientRepository.UpdateAsync(model);

			return result;
		}

		// DELETE api/<ClientSQLController>/5
		[HttpDelete("{id}")]
		public async Task<bool> DeleteAsync(int id)
		{
			var client = await clientRepository.GetAsync(id);
			var result = await clientRepository.DeleteAsync(client);

			return result;
		}
	}
}
