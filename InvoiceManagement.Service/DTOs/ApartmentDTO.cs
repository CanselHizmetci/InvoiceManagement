using System.Collections.Generic;

namespace InvoiceManagement.Service.DTOs
{
    public class ApartmentDTO
    {
        public int Id { get; set; }
        public ApartmentTypeDTO Type { get; set; }
        public int TypeId { get; set; }
        public bool Status { get; set; }
        public int Floor { get; set; }
        public int ApartmentNumber { get; set; }
        public UserDTO User { get; set; }
        public string UserId { get; set; }
        public BlockDTO Block { get; set; }
        public int BlockId { get; set; }
        public virtual ICollection<DebtDTO> Debts { get; set; }
    }
}
