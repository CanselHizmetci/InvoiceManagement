using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PaymentServiceApi.Models
{
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string FullName { get; set; } = null!;
        public string CreditCardNo { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
    }
}
