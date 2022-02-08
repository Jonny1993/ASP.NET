using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.ViewModels
{
    public class DeleteViewModel
    {
        public int messageId { get; set; }
        public string title { get; set; }

        public string content { get; set; }
        public DateTime timeSent { get; set; }

        public string senderEmail { get; set; }

        public DeleteViewModel(int messageId, string title, string content, DateTime timeSent, string senderEmail)
        {
            this.messageId = messageId;
            this.title = title;
            this.content = content;
            this.timeSent = timeSent;
            this.senderEmail = senderEmail;
        }
    }
}
