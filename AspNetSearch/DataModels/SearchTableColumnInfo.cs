using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.DataModels
{
    public class SearchTableColumnInfo
    {

        public int Id { get; set; }

        public string TableName { get; set; }

        public string TableColumnName { get; set; }

        public string TableColumnDisplayName { get; set; }

    }
}