using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models
{
    public class SearchGroupModel
    {

        public string GroupColumn { get; set; }

        public SearchGroupModel(string column)
        {
            GroupColumn = column;
        }

    }
}