using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models.Search
{
    public class SaveSearchSettingRepositoryInMemory : ISaveSearchSettingRepository
    {

        public static Dictionary<int, DataModels.SaveSearchSetting> KeyValueSettings = new Dictionary<int, DataModels.SaveSearchSetting>();

        public static Dictionary<int, DataModels.SaveSearchSettingGroup> KeyValueGroups = new Dictionary<int, DataModels.SaveSearchSettingGroup>();

        public static Dictionary<int, DataModels.SaveSearchSettingSelect> KeyValueSelects = new Dictionary<int, DataModels.SaveSearchSettingSelect>();

        public static Dictionary<int, DataModels.SaveSearchSettingWhere> KeyValueWheres = new Dictionary<int, DataModels.SaveSearchSettingWhere>();

        public void SaveSearchSetting(SaveSearchSettingInput input)
        {

            var setting = new DataModels.SaveSearchSetting();
            setting.SearchSettingId = KeyValueSettings.Count + 1;
            setting.SearchSettingName = input.SearchSettingName;
            setting.SearchTableId = input.SearchTableId;

            var groups = new List<DataModels.SaveSearchSettingGroup>();
            foreach(var item in input.Groups)
            {
                var group = new DataModels.SaveSearchSettingGroup();
                group.SearchSettingId = setting.SearchSettingId;
                group.GroupColumnId = item.GroupColumnId;
                group.SearchSettingGroupId = KeyValueGroups.Count + 1;
                groups.Add(group);
                KeyValueGroups.Add(group.SearchSettingGroupId, group);
            }

            var wheres = new List<DataModels.SaveSearchSettingWhere>();
            foreach(var item in input.Wheres)
            {
                var where = new DataModels.SaveSearchSettingWhere();
                where.SearchSettingId = setting.SearchSettingId;
                where.SearchSettingWhereId = KeyValueWheres.Count + 1;
                where.WhereColumnId = item.WhereColumnId;
                where.WhereRange = (int)item.WhereRange;
                where.WhereValue = item.WhereValue;
                wheres.Add(where);
                KeyValueWheres.Add(where.SearchSettingWhereId, where);
            }

            var selects = new List<DataModels.SaveSearchSettingSelect>();
            foreach(var item in input.Selects)
            {
                var select = new DataModels.SaveSearchSettingSelect();
                select.SearchSettingId = setting.SearchSettingId;
                select.SearchSettingSelectId = KeyValueSelects.Count + 1;
                select.SelectColumnId = item.SelectColumnId;
                select.SearchSelectValue = (int)item.SearchSelectValue;
                selects.Add(select);
                KeyValueSelects.Add(select.SearchSettingSelectId, select);
            }

            setting.Groups = groups;
            setting.Selects = selects;
            setting.Wheres = wheres;
            KeyValueSettings.Add(setting.SearchSettingId, setting);

        }
    }
}