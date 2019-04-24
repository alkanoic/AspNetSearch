using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models.Search
{
    public interface ISaveSearchSettingRepository
    {
        void SaveSearchSetting(SaveSearchSettingInput input);

        FetchAllSettingOutput FetchAllSetting();

        FetchSettingOutput FetchSetting(FetchSettingInput input);
    }

    public class FetchAllSettingOutput
    {
        public IEnumerable<FetchAllSettingDetailOutput> Details { get; set; }
    }

    public class FetchAllSettingDetailOutput
    {
        public int SearchSettingId { get; set; }

        public int TableId { get; set; }

        public string SearchSettingName { get; set; }
    }

    public class FetchSettingInput
    {
        public int SearchSettingId { get; set; }
    }

    public class FetchSettingOutput
    {
        public IEnumerable<SaveSearchWhereOutput> Wheres { get; set; }

        public IEnumerable<SaveSearchGroupOutput> Groups { get; set; }

        public IEnumerable<SaveSearchSelectOutput> Selects { get; set; }

    }

    public class SaveSearchWhereOutput
    {
        public int WhereColumnId { get; set; }

        public string WhereColumnName { get; set; }

        public string WhereColumnDisplayName { get; set; }

        public string WhereValue { get; set; }

        public WhereRangeEnum WhereRange { get; set; }

    }

    public class SaveSearchGroupOutput
    {
        public int GroupColumnId { get; set; }

        public string GroupColumnName { get; set; }

        public string GroupColumnDisplayName { get; set; }
    }

    public class SaveSearchSelectOutput
    {
        public int SelectColumnId { get; set; }

        public string SelectColumnName { get; set; }

        public string SelectColumnDisplayName { get; set; }

        public SelectValueEnum SearchSelectValue { get; set; }

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