using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models.Search
{
    public interface ISaveSearchSettingRepository
    {
        void SaveSearchSetting(SaveSearchSettingInput input);
    }

    public class SaveSearchSettingInput
    {

        public string SearchSettingName { get; set; }

        public int SearchTableId { get; set; }

        public IEnumerable<SaveSearchWhereInput> Wheres { get; set; }

        public IEnumerable<SaveSearchGroupInput> Groups { get; set; }

        public IEnumerable<SaveSearchSelectInput> Selects { get; set; }

    }

    public class SaveSearchWhereInput
    {
        public int WhereColumnId { get; set; }

        public string WhereValue { get; set; }

        public WhereRangeEnum WhereRange { get; set; }

    }

    public class SaveSearchGroupInput
    {
        public int GroupColumnId { get; set; }

    }

    public class SaveSearchSelectInput
    {
        public int SelectColumnId { get; set; }

        public SelectValueEnum SearchSelectValue { get; set; }

    }

}