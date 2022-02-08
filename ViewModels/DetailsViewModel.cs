using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.ViewModels
{
    public class DetailsViewModel
    {
        public int messageId { get; set; }
        public string title { get; set; }

        public string content { get; set; }
        public DateTime timeSent { get; set; }

        public string senderEmail { get; set; }

        public bool isRead { get; set; }
        public DetailsViewModel(int messageId, string title, string content, DateTime timeSent, string senderEmail, bool isRead)
        {
            this.messageId = messageId;
            this.title = title;
            this.content = content;
            this.timeSent = timeSent;
            this.senderEmail = senderEmail;
            this.isRead = isRead;
        }
    }
}
