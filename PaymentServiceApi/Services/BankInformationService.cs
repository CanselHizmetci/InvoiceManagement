using System.Collections.Generic;
using MongoDB.Driver;
using PaymentServiceApi.Models;

namespace PaymentServiceApi.Services
{
    public class BankInformationService
    {
        private readonly IMongoCollection<BankInformation> _bankInformation;
        public BankInformationService(IPaymentServiceDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _bankInformation = database.GetCollection<BankInformation>(settings.BankInfoCollectionName);
        }
        public List<BankInformation> Get() =>
             _bankInformation.Find(c => true).ToList();

        public BankInformation Get(string id) =>
            _bankInformation.Find<BankInformation>(c => c.Id == id).FirstOrDefault();

        public BankInformation Create(BankInformation bankInformation)
        {
            bankInformation.Id = null;
            _bankInformation.InsertOne(bankInformation);
            return bankInformation;
        }

        public void Update(string id, BankInformation bankIn) =>
            _bankInformation.ReplaceOne(c => c.Id == id, bankIn);

        public void Remove(BankInformation bankIn) =>
            _bankInformation.DeleteOne(c => c.Id == bankIn.Id);

        public void Remove(string id) =>
            _bankInformation.DeleteOne(c => c.Id == id);
    }
}
