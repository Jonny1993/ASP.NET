using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social.Areas.Identity.Data;
using Social.Data;
using Social.Models;
using Social.ViewModels;

namespace Social.BusinessObjects
{
    public class MessageHandler
    {
        private readonly SocialContext _context;
        private readonly UserManager<SocialUser> _userManager;
        private readonly SocialIDContext _idContext;

        public MessageHandler(SocialContext context, UserManager<SocialUser> userManager, SocialIDContext idContext)
        {
            _context = context;
            _userManager = userManager;
            _idContext = idContext;
        }

        public List<string> getUniqueSenders(string userID)
        {
            List<Message> emails = _context.Messages.Where(message => message.ReceiverID.Equals(userID)).ToList();
            IEnumerable<string> uniqueSenders = emails.Select(message => message.SenderID).Distinct();
            List<string> senderEmails = new List<string>();
            foreach (string thisID in uniqueSenders)
            {
                senderEmails.Add(getEmail(thisID));
            }
            return senderEmails;
        }

        public List<Message> getMessages(string userID, string senderEmail)
        {
            List<Message> messages = _context.Messages.Where(message => message.ReceiverID.Equals(userID) && message.SenderID.Equals(getID(senderEmail))).ToList();
            return messages;
        }

        public int nrOfMessages(string userID)
        {
            return _context.Messages.Count(m => m.ReceiverID.Equals(userID) && m.IsRead);
        }

        public Message getMessage(int id)
        {
            var message = _context.Messages.Where(m => m.MessageId == id).ToList();
            return message.First();
        }

        public void createMessage(Message m)
        {
            _context.Messages.Add(m);
            _context.SaveChanges();
        }

        public IEnumerable<string> getEmailList()
        {
            var users = _userManager.Users.Where(user => user.EmailConfirmed == true).ToList();
            IEnumerable<string> emailList = users.Select(user => user.Email);
            return emailList;
        }

        public void setToRead(Message m)
        {
            m.IsRead = true;
            _context.Messages.Update(m);
            _context.SaveChanges();
        }

        public void deleteMessage(int id)
        {
            Message m = getMessage(id);
            _context.Messages.Remove(m);
            UserHandler uH = new UserHandler(_userManager, _idContext);
            uH.incrementDelete(m.ReceiverID);
            _context.SaveChanges();
        }

        public string getEmail(string usrId)
        {
            var thisUsrAsList = _userManager.Users.Where(user => user.Id.Equals(usrId));
            var thisUsr = thisUsrAsList.First();
            return thisUsr.Email;

        } 

        private string getID(string email)
        {
            var thisSender = _userManager.Users.Where(user => user.Email.Equals(email));
            var thisSenderID = thisSender.First().Id;
            return thisSenderID;
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }

    }
}
