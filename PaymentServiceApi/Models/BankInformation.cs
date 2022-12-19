using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PaymentServiceApi.Models
{
    public class BankInformation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string FullName { get; set; } = null!;
        public string CreditCardNo { get; set; }
        public int Cvv { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal Balance { get; set; }
        
    }

    
}
