using System;
using System.Collections.Generic;

namespace InvoiceManagement.Service.DTOs
{
    public class DebtDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueTime { get; set; }
        public bool IsPaid { get; set; }
        public virtual ICollection<PaymentDTO> Payments { get; set; }
        public int ApartmentId { get; set; }
        public ApartmentDTO Apartment { get; set; }
    }
}
