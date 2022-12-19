namespace PaymentServiceApi.Models
{
    public class PaymentServiceDbSettings:IPaymentServiceDbSettings
    {
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; } 

        public string PaymentsCollectionName { get; set; } 
        public string BankInfoCollectionName { get; set; } 
    }
    public interface IPaymentServiceDbSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; } 

        string PaymentsCollectionName { get; set; } 
        string BankInfoCollectionName { get; set; } 
    }
}
