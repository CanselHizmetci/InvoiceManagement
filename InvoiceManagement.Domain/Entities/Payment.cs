using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Entities
{
    public class Payment:BaseEntity
    {
        public string CreditCardNo { get; set; }
        [Required, ForeignKey("DebtId")]
        public virtual Debt Debt { get; set; }
        public int DebtId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        [Required, ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
