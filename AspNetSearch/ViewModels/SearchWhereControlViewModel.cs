using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchWhereControlViewModel
    {

        public string SearchId { get; set; }

        public string SearchWhereName { get; set; }

        public string SearchDisplayName { get; set; }

        [Required]
        public string SearchValue { get; set; }

        [Required]
        public int SearchRange { get; set; }

    }
}