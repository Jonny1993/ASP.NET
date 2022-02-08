using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Social.Models;

namespace Social.ViewModels
{
    public class MessageListViewModel
    {
        public int messageID { get; set; }
        public string sender { get; set; }
        public string title { get; set; }
        public DateTime timeSent { get; set; }
            public MessageListViewModel(int messageID, string sender, string title, DateTime timeSent)
        {
            this.messageID = messageID;
            this.sender = sender;
            this.title = title;
            this.timeSent = timeSent;
        }
            
        
    }
}
