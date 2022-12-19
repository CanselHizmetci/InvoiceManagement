namespace PaymentServiceApi.Models
{
    public class PayModel
    {
        public string FullName { get; set; } = null!;
        public string CreditCardNo { get; set; }
        public int Cvv { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal Amount { get; set; }
    }
}
