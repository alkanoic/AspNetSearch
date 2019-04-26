using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models
{
    public class SearchModel
    {

        public List<SearchWhereModel> WhereModel { get; set; }

        public List<SearchSelectModel> SelectModel { get; set; }
    }
}