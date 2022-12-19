using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Entities
{
    public class User:BaseEntity
    {
        public string  Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LicensePlate { get; set; }
        public bool HaveAVehicle { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
