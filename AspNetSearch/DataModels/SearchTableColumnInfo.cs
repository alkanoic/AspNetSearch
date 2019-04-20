using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.DataModels
{
    public class SearchTableColumnInfo
    {

        public int TableId { get; set; }

        public int ColumnId { get; set; }

        public string TableColumnName { get; set; }

        public string TableColumnDisplayName { get; set; }

    }
}