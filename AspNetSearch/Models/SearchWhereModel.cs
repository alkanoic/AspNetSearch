using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models
{
    public class SearchWhereModel
    {

        public string WhereColumn { get; set; }

        public int WhereRange { get; set; }

        public string WhereValue { get; set; }


        public SearchWhereModel(string column, int range, string value)
        {
            WhereColumn = column;
            WhereRange = range;
            WhereValue = value;
        }

    }
}