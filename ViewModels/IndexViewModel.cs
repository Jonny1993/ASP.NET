using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.ViewModels
{
    public class IndexViewModel
    {
        public List<string> senderEmails { get; set; }

        public int nrMessagesRead, nrDeletedMessages;

        public IndexViewModel(List<string> senderEmails, int nrMessagesRead, int nrDeletedMessages)
        {
            this.senderEmails = senderEmails;
            this.nrMessagesRead = nrMessagesRead;
            this.nrDeletedMessages = nrDeletedMessages;
        }
    }
}
