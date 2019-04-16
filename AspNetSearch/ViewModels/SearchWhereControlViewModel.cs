using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchWhereControlViewModel
    {

        public int SearchId { get; set; }

        public string SearchName { get; set; }

        [Required]
        public string SearchValue { get; set; }

        [Required]
        public int SearchRange { get; set; }

    }
}