using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.NoSQLEntities
{
	[BsonIgnoreExtraElements]
	public class Client 
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("lastName")]
		public string LastName { get; set; }

		[BsonElement("age")]
		public int Age { get; set; }

		[BsonElement("state")]
		public string State { get; set;	}

	}
}
