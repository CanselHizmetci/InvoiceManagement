using System.Collections.Generic;

namespace InvoiceManagement.Service.DTOs
{
    public class ApartmentTypeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ApartmentDTO> Apartments { get; set; }
    }
}
