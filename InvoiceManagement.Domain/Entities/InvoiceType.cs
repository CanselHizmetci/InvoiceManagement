using System.Collections.Generic;

namespace InvoiceManagement.Domain.Entities
{
    public class InvoiceType : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
