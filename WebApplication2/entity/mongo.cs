using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace WebApplication2.entity
{
    public class mongo
    {
        [BsonElement("Name")]

        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int phone { get; set; }

    }
}
