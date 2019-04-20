using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchIndexViewModel
    {

        public IEnumerable<SearchIndexDetail> Details { get; set; }

    }

    public class SearchIndexDetail
    {
        public int TableId { get; set; }

        public string TableDisplayName { get; set; }

    }
}