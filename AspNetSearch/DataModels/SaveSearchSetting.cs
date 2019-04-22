using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.DataModels
{
    public class SaveSearchSetting
    {

        public int SearchSettingId { get; set; }

        public string SearchSettingName { get; set; }

        public int SearchTableId { get; set; }


        public IEnumerable<SaveSearchSettingGroup> Groups { get; set; }

        public IEnumerable<SaveSearchSettingSelect> Selects { get; set; }

        public IEnumerable<SaveSearchSettingWhere> Wheres { get; set; }

    }
}