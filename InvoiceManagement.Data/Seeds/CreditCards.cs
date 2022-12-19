using InvoiceManagement.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagement.Data.Seeds
{
    public class CreditCards
    {
        public static async Task SeedCreditCard()
        {
            using var client = new HttpClient();
            var cardListStr = await client.GetStringAsync("https://localhost:44352/api/BankInformation");
            var cardList = JsonConvert.DeserializeObject<IList<CreditCardModel>>(cardListStr);
            if (cardList.All(c => c.CreditCardNo != "1111222233334444"))
                await client.PostAsync(new Uri("https://localhost:44352/api/BankInformation"), new StringContent(JsonConvert.SerializeObject(new
                    CreditCardModel
                {
                    FullName = "Owner Name",
                    CreditCardNo = "1111222233334444",
                    Cvv = 123,
                    ExpirationMonth = 10,
                    ExpirationYear = 24,
                    Balance = 1000
                }), Encoding.UTF8, "application/json"));
            if (cardList.All(c => c.CreditCardNo != "1234567812345678"))
                await client.PostAsync(new Uri("https://localhost:44352/api/BankInformation"), new StringContent(JsonConvert.SerializeObject(new
                CreditCardModel
                {
                    FullName = "Owner Name",
                    CreditCardNo = "1234567812345678",
                    Cvv = 123,
                    ExpirationMonth = 10,
                    ExpirationYear = 26,
                    Balance = 5000
                }), Encoding.UTF8, "application/json"));
        }

        public class CreditCardModel
        {
            public string FullName { get; set; }
            public string CreditCardNo { get; set; }
            public int Cvv { get; set; }
            public int ExpirationMonth { get; set; }
            public int ExpirationYear { get; set; }
            public decimal Balance { get; set; }
        }

    }
}
