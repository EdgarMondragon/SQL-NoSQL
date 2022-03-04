using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Base
{
    public interface IEntityBase
    {
        int Id { get; set; }
    }
}
