using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Entities
{
    public class Apartment:BaseEntity
    {
        [Required, ForeignKey("TypeId")]
        public virtual ApartmentType Type { get; set; }
        public int TypeId { get; set; }
        public int BlockId { get; set; }
        public bool Status { get; set; }
        public int Floor { get; set; }
        public int ApartmentNumber { get; set; }
        public string UserId { get; set; }
        [Required,ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [Required, ForeignKey("BlockId")]
        public virtual Block Block { get; set; }
        public virtual ICollection<Debt> Debts { get; set; }
    }
}
