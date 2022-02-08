using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Social.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Contents { get; set; }
        [DataType(DataType.Date)]
        public DateTime timeSent { get; set; } = DateTime.Now;
        [Required]
        public string SenderID { get; set; }
        [Required]
        public string ReceiverID { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
