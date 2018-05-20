using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAS_Rental_DVD_Kel_3.Entities
{
    class Anggota
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("address2")]
        public string Address2 { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("postal_zip")]
        public string PostalZip { get; set; }
    }
}
