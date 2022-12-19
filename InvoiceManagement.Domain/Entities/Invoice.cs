using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Entities
{
    public class Invoice:BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set;}
        public DateTime InvoiceReadDate { get; set; }
        [Required, ForeignKey("InvoiceTypeId")]
        public virtual InvoiceType Type { get; set; }
        public int InvoiceTypeId { get; set; }
        public DateTime DueTime { get; set; }

    }
}
