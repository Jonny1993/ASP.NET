using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Areas.Identity.Data
{
    public class LogIn
    {
        [Key]
        public int LogInId { get; set; }
        [ForeignKey("Id")]
        public string UserID { get; set; }
        public DateTime LoginDate { get; set; }

    }
}
