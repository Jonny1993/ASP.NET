using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Social.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string name, DateTime date, int logIns)
        {
            this.name = name;
            this.lastLogin = date;
            this.logInsLast30Days = logIns;
        }
        public string name { set; get; }
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd at HH:mm}")]
        public DateTime lastLogin { set; get; }
        public int logInsLast30Days { set; get; }
    }
}
