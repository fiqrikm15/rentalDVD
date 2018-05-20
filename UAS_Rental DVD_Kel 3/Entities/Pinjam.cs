using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAS_Rental_DVD_Kel_3.Entities
{
    class Pinjam
    {
        [BsonId]
        public ObjectId id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("member_name")]
        public string MemberName { get; set; }

        [BsonElement("rental_date")]
        public DateTime RentalDate { get; set; }

        [BsonElement("return_date")]
        public DateTime ReturnDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}
