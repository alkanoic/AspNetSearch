using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models.Search
{
    public class SaveSearchSettingRepositoryInMemory : ISaveSearchSettingRepository
    {

        public static Dictionary<int, DataModels.SaveSearchSetting> KeyValueSettings = new Dictionary<int, DataModels.SaveSearchSetting>();

        public static Dictionary<int, DataModels.SaveSearchSettingSelect> KeyValueSelects = new Dictionary<int, DataModels.SaveSearchSettingSelect>();

        public static Dictionary<int, DataModels.SaveSearchSettingWhere> KeyValueWheres = new Dictionary<int, DataModels.SaveSearchSettingWhere>();

        public FetchAllSettingOutput FetchAllSetting()
        {
            var output = new FetchAllSettingOutput();
            var list = new List<FetchAllSettingDetailOutput>();
            foreach(var item in KeyValueSettings.Values)
            {
                var detail = new FetchAllSettingDetailOutput()
                {
                    SearchSettingId = item.SearchSettingId,
                    SearchSettingName = item.SearchSettingName,
                    TableId = item.SearchTableId
                };
                list.Add(detail);
            }
            output.Details = list;
            return output;
        }

        public FetchSettingOutput FetchSetting(FetchSettingInput input)
        {
            var output = new FetchSettingOutput();
            var wheres = new List<SaveSearchWhereOutput>();
            foreach(var item in KeyValueWheres.Values.Where(x => x.SearchSettingId == input.SearchSettingId))
            {
                var result = FetchTableInfoRepositoryInMemory.KeyValueColumns.Values.Single(x => x.ColumnId == item.WhereColumnId);
                var w = new SaveSearchWhereOutput()
                {
                    WhereColumnId = item.WhereColumnId,
                    WhereRange = (WhereRangeEnum)item.WhereRange,
                    WhereValue = item.WhereValue,
                    WhereColumnName = result.TableColumnName,
                    WhereColumnDisplayName = result.TableColumnDisplayName
                };
                wheres.Add(w);
            }

            var selects = new List<SaveSearchSelectOutput>();
            foreach (var item in KeyValueSelects.Values.Where(x => x.SearchSettingId == input.SearchSettingId))
            {
                var result = FetchTableInfoRepositoryInMemory.KeyValueColumns.Values.Single(x => x.ColumnId == item.SelectColumnId);
                var s = new SaveSearchSelectOutput()
                {
                    SelectColumnId = item.SelectColumnId,
                    SearchSelectValue = (SelectValueEnum)item.SearchSelectValue,
                    SelectColumnDisplayName = result.TableColumnDisplayName,
                    SelectColumnName = result.TableColumnName
                };
                selects.Add(s);
            }

            output.Wheres = wheres;
            output.Selects = selects;
            return output;
        }

        public void SaveSearchSetting(SaveSearchSettingInput input)
        {

            var setting = new DataModels.SaveSearchSetting();
            setting.SearchSettingId = KeyValueSettings.Count + 1;
            setting.SearchSettingName = input.SearchSettingName;
            setting.SearchTableId = input.SearchTableId;

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

            setting.Selects = selects;
            setting.Wheres = wheres;
            KeyValueSettings.Add(setting.SearchSettingId, setting);

        }
    }
}