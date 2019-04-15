using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models
{
    public class SearchSelectModel
    {

        public string SelectColumn { get; set; }

        public int SelectValue { get; set; }

        public SearchSelectModel(string column, int value)
        {
            SelectColumn = column;
            SelectValue = value;
        }

    }
}