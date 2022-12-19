using System;

namespace InvoiceManagement.Service.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public UserDTO Sender { get; set; }
        public bool IsReaded { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsOutgoing { get; set; }
        public string SenderId { get; set; }
    }
}
