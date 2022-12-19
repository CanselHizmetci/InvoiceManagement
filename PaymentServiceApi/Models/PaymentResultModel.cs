namespace PaymentServiceApi.Models
{
    public class PaymentResultModel
    {
        public bool Result { get; set; }
        public string? ErrorMessage { get; set; }
        public string CreditCardNo { get; set; }
    }
}
