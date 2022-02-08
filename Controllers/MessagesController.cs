using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Social.Data;
using Social.Models;
using Social.Areas.Identity.Data;
using Social.ViewModels;
using Social.BusinessObjects;

namespace Social
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly MessageHandler messageHandler;
        private readonly UserHandler userHandler;

        public MessagesController(UserManager<SocialUser> userManager, SocialContext context, SocialIDContext idContext)
        {
            messageHandler = new MessageHandler(context, userManager, idContext);
            userHandler = new UserHandler(userManager, idContext);
        }

        // GET: Senders
        public IActionResult Index()
        {
            var senders = messageHandler.getUniqueSenders(userHandler.getId(User));
            int nrMessagesRead = messageHandler.nrOfMessages(userHandler.getId(User));
            int nrDeletedMessages = userHandler.nrOfDeletedMessages(userHandler.getId(User));
            IndexViewModel vm = new IndexViewModel(senders, nrMessagesRead, nrDeletedMessages);
            return View(vm);
        }
        // GET: Messages/MessageList?sender=email@domain.com
        public IActionResult MessageList(string sender)
        {
            if (sender == null)
            {
                return NotFound();
            }

            var messages = messageHandler.getMessages(userHandler.getId(User), sender);

            List<MessageListViewModel> messageListVM = new List<MessageListViewModel>();

            foreach(Message m in messages)
            {
                messageListVM.Add(new MessageListViewModel(m.MessageId, messageHandler.getEmail(m.SenderID),m.Title, m.timeSent));
            }


            return View(messageListVM);
        }

        // GET: Messages/Details/5
        public IActionResult Details(int id)
        {
            Message m = messageHandler.getMessage(id);
            if (m == null)
            {
                return NotFound();
            }
            DetailsViewModel messageVM = new DetailsViewModel(m.MessageId, m.Title, m.Contents, m.timeSent, messageHandler.getEmail(m.SenderID), m.IsRead);
            if (isCorrectUser(m.ReceiverID))
            {
                messageHandler.setToRead(m);
                return View(messageVM);
            }
            return NotFound();
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            IEnumerable<string> emailList = messageHandler.getEmailList();

            CreateViewModel vm = new CreateViewModel
            {
                Title = "",
                Content = "",
                Email = new SelectList(emailList, "Email"),
            };
            return View(vm);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Email, string title, string content)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("The Message: " + title + " " + content + " " + Email);
                var messageToSend = new Message();
                messageToSend.SenderID = userHandler.getId(User);
                var receiver = userHandler.getUserByEmail(Email).Id;
                messageToSend.ReceiverID = receiver;
                messageToSend.Title = title;
                messageToSend.Contents = content;
                messageHandler.createMessage(messageToSend);
                ModelState.Clear();
                this.TempData["messages"] = "Message sent to " + Email + " at " + messageToSend.timeSent;
                return RedirectToAction("Create");
            }
            return NotFound();
        }

        // GET: Messages/Delete/5
        public IActionResult Delete(int id)
        {

            var message = messageHandler.getMessage(id);

            if (message == null)
            {
                return NotFound();
            }

            DeleteViewModel deleteVM = new DeleteViewModel(message.MessageId, message.Title, message.Contents, message.timeSent, messageHandler.getEmail(message.SenderID));

            if (isCorrectUser(message.ReceiverID))
            {
                return View(deleteVM);
            }
            return NotFound();
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            messageHandler.deleteMessage(id);
            return RedirectToAction(nameof(Index));
        }

        private Boolean isCorrectUser(string userID)
        {
            return userHandler.getId(User).Equals(userID);
        }
    }
} 
