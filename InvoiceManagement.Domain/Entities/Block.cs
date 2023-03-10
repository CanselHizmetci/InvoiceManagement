using System.Collections.Generic;

namespace InvoiceManagement.Domain.Entities
{
    public class Block:BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
