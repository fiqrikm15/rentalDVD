using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAS_Rental_DVD_Kel_3.Entities
{
    class Film
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("release_year")]
        public double ReleaseYear { get; set; }

        [BsonElement("language")]
        public string Language { get; set; }

        [BsonElement("length")]
        public double Length { get; set; }

        [BsonElement("rating")]
        public string Rating { get; set; }

        [BsonElement("original_language")]
        public string OriginalLanguage { get; set; }
    }
}
