using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchViewModel
    {

        public IEnumerable<SearchControlViewModel> Controls { get; set; }

    }
}