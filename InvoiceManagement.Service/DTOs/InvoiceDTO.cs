using System;

namespace InvoiceManagement.Service.DTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceReadDate { get; set; }
        public InvoiceTypeDTO Type { get; set; }
        public int InvoiceTypeId { get; set; }
        public DateTime DueTime { get; set; }

    }
}
