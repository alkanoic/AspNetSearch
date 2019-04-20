using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchViewModel
    {

        public int TableId { get; set; }

        public SearchViewModel(int tableId)
        {
            TableId = tableId;
        }

    }
}