using System.Collections.Generic;

namespace InvoiceManagement.Service.DTOs
{
    public class InvoiceTypeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<InvoiceDTO> Invoices { get; set; }
    }
}
