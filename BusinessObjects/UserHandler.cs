using Social.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Social.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Social.Models;
using Social.ViewModels;

namespace Social.BusinessObjects
{
    public class UserHandler
    {
        private readonly UserManager<SocialUser> _userManager;
        private readonly SocialIDContext _socialIDContext;


        // Summary:
        //     Initializes a new instance with dependency injection for DBContex for both Identitiy as well as UserManager databases
        //
        // Parameters:
        //   myUserManager:
        //   socialIDContext
        public UserHandler(UserManager<SocialUser> myUserManager, SocialIDContext socialIDContext)
        {
            _userManager = myUserManager;
            _socialIDContext = socialIDContext;
        }

        // Summary:
        //     Returns true if msg passed as  param.1 blong to same user passed as param.2
        //
        // Parameters:
        //   msg:
        //   usr
        public bool isItemByCurrentUser(Message msg, ClaimsPrincipal usr)
        {

            var currentUserId = _userManager.GetUserId(usr);
            return msg.MessageId.ToString() == currentUserId;
        }
        // Summary:
        //     Returns ID of current userpassed as param.
        //
        // Parameters:
        //   usr
        public string getId(ClaimsPrincipal usr)
        {
            var id = _userManager.GetUserId(usr);
            return id;

        }
        // Summary:
        //     Returns Email of current user passed by his/her ID as param.
        //
        // Parameters:
        //   usrId
        public string getEmail(string usrId)
        {
            var thisUsrAsList = _userManager.Users.Where(user => user.Id.Equals(usrId));
            var thisUsr = thisUsrAsList.First();
            return thisUsr.Email;

        }

        public int nrOfDeletedMessages(string userID)
        {
            var thisUserAsList = _socialIDContext.Users.Where(user => user.Id.Equals(userID));
            return thisUserAsList.First().nrOfDeletedMessages;
        }

        public void incrementDelete(string userID)
        {
            var user = _socialIDContext.Users.Where(user => user.Id.Equals(userID)).First();
            user.nrOfDeletedMessages++;
            _socialIDContext.Users.Update(user);
            _socialIDContext.SaveChanges();
        }
        // Summary:
        //     Returns user that has the email passed as param.
        //
        // Parameters:
        //   email
        public SocialUser getUserByEmail(string email)
        {
            return _userManager.Users.Where(user => user.Email.Equals(email)).First();
        }
        // Summary:
        //     Returns userVM of current user
        //
        // Parameters:
        //   usr
        public UserViewModel getUserVM(ClaimsPrincipal usr)
        {
            //Reading data from database
            var thisUsrAsList = _userManager.Users.Where(user => user.Email.Equals(usr.Identity.Name));
            var thisUsr = thisUsrAsList.First();
            List<LogIn> LogInsList = _socialIDContext.LogIns.Where(logIn => logIn.UserID.Equals(thisUsr.Id)).ToList();

            //Converting object to VMObject
            DateTime lastLogIn;
            int logInsCountLast30 = 0;
            var today = DateTime.Now;
            var last31DaysStartDate = today.AddDays(-31);
            //Loops through past log in in order to count nr. of log  in last 30 days
            if (LogInsList.Count>1) 
            {
                foreach (LogIn lg in LogInsList)
                {
                    if (lg.LoginDate.CompareTo(last31DaysStartDate) > 0) {
                        logInsCountLast30++;
                    }
                }

                lastLogIn = LogInsList[LogInsList.Count - 2].LoginDate;
               
            }
            //Ifthis is first log in
            else
            {
                logInsCountLast30 = 1;
                lastLogIn = DateTime.Now;

            }
            //Create userVM object
            var userVM = new UserViewModel(thisUsr.UserName, lastLogIn, logInsCountLast30);
            return userVM;
        }
        
    }
}
