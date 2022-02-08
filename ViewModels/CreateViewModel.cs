using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Social.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Content {get; set; }

        public IEnumerable<SelectListItem> Email { get; set; }

    }

}
