using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Entities
{
    public class Message:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [Required,ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
        public string SenderId { get; set; }
        public bool IsReaded { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsOutgoing { get; set; }
    }
}
