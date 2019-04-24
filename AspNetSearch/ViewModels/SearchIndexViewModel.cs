using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.ViewModels
{
    public class SearchIndexViewModel
    {

        public IEnumerable<SearchIndexDetail> Details { get; set; }

        public IEnumerable<SaveSearchSettingDetail> SaveDetails { get; set; }

    }

    public class SearchIndexDetail
    {
        public int TableId { get; set; }

        public string TableDisplayName { get; set; }

    }

    public class SaveSearchSettingDetail
    {
        public int SearchSettingId { get; set; }

        public int TableId { get; set; }

        public string SearchSettingName { get; set; }
    }
}