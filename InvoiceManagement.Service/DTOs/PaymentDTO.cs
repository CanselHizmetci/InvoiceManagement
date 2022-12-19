using System;

namespace InvoiceManagement.Service.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public string CreditCardNo { get; set; }
        public DebtDTO Debt { get; set; }
        public int DebtId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
