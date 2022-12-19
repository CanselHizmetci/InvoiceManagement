using System.Collections.Generic;

namespace InvoiceManagement.Service.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LicensePlate { get; set; }
        public bool HaveAVehicle { get; set; }
        public virtual ICollection<ApartmentDTO> Apartments { get; set; }
        public virtual ICollection<MessageDTO> Messages { get; set; }
    }
}
