using MongoDB.Driver;
using PaymentServiceApi.Models;

namespace PaymentServiceApi.Services
{
    public class PaymentService
    {
        private readonly IMongoCollection<Payment> _payment;
        public PaymentService(IPaymentServiceDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _payment = database.GetCollection<Payment>(settings.PaymentsCollectionName);
        }

        public Payment Create(Payment payment)
        {
            payment.Id = null;
            _payment.InsertOne(payment);
            return payment;

        }
    }
}
