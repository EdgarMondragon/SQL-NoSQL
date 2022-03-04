using Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.SQLEntities
{
	public class Client : IEntityBase
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string State { get; set;	}
		
	}
}
