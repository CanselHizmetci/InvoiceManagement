using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Entities
{
    public class Debt:BaseEntity
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueTime { get; set; }
        public bool IsPaid { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        [Required, ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }
        public int ApartmentId { get; set; }

    }
}
