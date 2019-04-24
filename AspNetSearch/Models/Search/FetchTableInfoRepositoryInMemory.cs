using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models.Search
{
    public class FetchTableInfoRepositoryInMemory : IFetchTableInfoRepository
    {

        public static Dictionary<int, DataModels.SearchTableInfo> KeyValueTables { get; set; }

        public static Dictionary<int, DataModels.SearchTableColumnInfo> KeyValueColumns { get; set; }

        public FetchTableInfoRepositoryInMemory()
        {
            KeyValueTables = new Dictionary<int, DataModels.SearchTableInfo>();
            var info1 = new DataModels.SearchTableInfo();
            info1.Id = 1;
            info1.TableDisplayName = "テーブル１";
            info1.TableName = "Table1";
            KeyValueTables.Add(info1.Id, info1);

            var info2 = new DataModels.SearchTableInfo();
            info2.Id = 2;
            info2.TableDisplayName = "テーブル２";
            info2.TableName = "Table2";
            KeyValueTables.Add(info2.Id, info2);

            KeyValueColumns = new Dictionary<int, DataModels.SearchTableColumnInfo>();
            var column1 = new DataModels.SearchTableColumnInfo();
            column1.TableId = 1;
            column1.ColumnId = 1;
            column1.TableColumnName = "Column1";
            column1.TableColumnDisplayName = "列名1";
            KeyValueColumns.Add(column1.ColumnId, column1);

            var column2 = new DataModels.SearchTableColumnInfo();
            column2.TableId = 1;
            column2.ColumnId = 2;
            column2.TableColumnName = "column2";
            column2.TableColumnDisplayName = "列名2";
            KeyValueColumns.Add(column2.ColumnId, column2);

            var column3 = new DataModels.SearchTableColumnInfo();
            column3.TableId = 1;
            column3.ColumnId = 3;
            column3.TableColumnName = "column3";
            column3.TableColumnDisplayName = "列名3";
            KeyValueColumns.Add(column3.ColumnId, column3);

            var column4 = new DataModels.SearchTableColumnInfo();
            column4.TableId = 2;
            column4.ColumnId = 4;
            column4.TableColumnName = "column21";
            column4.TableColumnDisplayName = "列名2-1";
            KeyValueColumns.Add(column4.ColumnId, column4);

            var column5 = new DataModels.SearchTableColumnInfo();
            column5.TableId = 2;
            column5.ColumnId = 5;
            column5.TableColumnName = "column22";
            column5.TableColumnDisplayName = "列名2-2";
            KeyValueColumns.Add(column5.ColumnId, column5);
        }

        public FetchAllTableInfoOutput FetchAllTableInfo(FetchAllTableInfoInput intput)
        {
            var output = new FetchAllTableInfoOutput();
            var list = new List<FetchTableDetailInfo>();

            foreach(var item in KeyValueTables.Values)
            {
                list.Add(new FetchTableDetailInfo(item.Id, item.TableDisplayName, item.TableName));
            }

            output.FetchTableDetails = list;
            return output;
        }

        public FetchTableAllColumnInfoOutput FetchTableAllColumnInfo(FetchTableAllColumnInfoInput input)
        {
            var output = new FetchTableAllColumnInfoOutput();
            var list = new List<FetchTableColumnDetailInfo>();

            foreach(var item in KeyValueColumns.Values.Where(i => i.TableId == input.TableId))
            {
                var i = new FetchTableColumnDetailInfo();
                i.ColumnId = item.ColumnId;
                i.ColumnName = item.TableColumnName;
                i.ColumnDisplayName = item.TableColumnDisplayName;
                list.Add(i);
            }

            output.FetchTableColumnDetails = list;
            return output;
        }

        public FetchTableInfoOutput FetchTableInfo(FetchTableInfoInput input)
        {
            var output = new FetchTableInfoOutput();
            DataModels.SearchTableInfo value;
            KeyValueTables.TryGetValue(input.TableId, out value);
            if (value == null) return null;
            output.TableInfo = new FetchTableDetailInfo(value.Id, value.TableDisplayName, value.TableName);
            return output;
        }

        public FetchTableColumnInfoOutput FetchTableColumnInfo(FetchTableColumnInfoInput input)
        {
            var output = new FetchTableColumnInfoOutput();
            output.ColumnInfo = new FetchTableColumnDetailInfo();

            DataModels.SearchTableColumnInfo value;
            if(KeyValueColumns.TryGetValue(input.ColumnId, out value))
            {
                output.ColumnInfo.ColumnId = value.ColumnId;
                output.ColumnInfo.ColumnName = value.TableColumnName;
                output.ColumnInfo.ColumnDisplayName = value.TableColumnDisplayName;
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}