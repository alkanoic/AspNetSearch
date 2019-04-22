using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.DataModels
{
    public class SaveSearchSettingWhere
    {

        public int SearchSettingWhereId { get; set; }

        public int SearchSettingId { get; set; }

        public int WhereColumnId { get; set; }

        public string WhereValue { get; set; }

        public int WhereRange { get; set; }

    }
}