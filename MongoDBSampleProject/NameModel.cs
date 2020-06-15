using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBSampleProject
{
    [BsonIgnoreExtraElements]
    public class NameModel
    {
        [BsonId]
        public Guid Id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
