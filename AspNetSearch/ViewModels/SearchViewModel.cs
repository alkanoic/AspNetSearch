using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchViewModel
    {

        public int TableId { get; set; }

        public IEnumerable<SearchTableColumn> Columns { get; set; }

        public SearchViewModel(int tableId)
        {
            TableId = tableId;
        }

    }

    public class SearchTableColumn
    {
        public int ColumnId { get; set; }

        public string ColumnName { get; set; }

        public string ColumnDisplayName { get; set; }

    }

}