using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models.Search
{
    public class FetchTableInfoRepositoryInMemory : IFetchTableInfoRepository
    {

        private static Dictionary<int, DataModels.SearchTableInfo> RepositoryValue { get; set; }

        public FetchTableInfoRepositoryInMemory()
        {
            RepositoryValue = new Dictionary<int, DataModels.SearchTableInfo>();
            var info1 = new DataModels.SearchTableInfo();
            info1.Id = 1;
            info1.TableDisplayName = "テーブル１";
            info1.TableName = "Table1";
            RepositoryValue.Add(info1.Id, info1);

            var info2 = new DataModels.SearchTableInfo();
            info2.Id = 2;
            info2.TableDisplayName = "テーブル２";
            info2.TableName = "Table2";
            RepositoryValue.Add(info2.Id, info2);
        }

        public FetchAllTableInfoOutput FetchAllTableInfo(FetchAllTableInfoInput intput)
        {
            var output = new FetchAllTableInfoOutput();
            var list = new List<FetchTableDetailInfo>();

            foreach(var item in RepositoryValue.Values)
            {
                list.Add(new FetchTableDetailInfo(item.Id, item.TableDisplayName, item.TableName));
            }

            output.FetchTableDetails = list;
            return output;
        }

        public FetchTableColumnInfoOutput FetchTableColumnInfo(FetchTableColumnInfoInput input)
        {
            var output = new FetchTableColumnInfoOutput();
            return output;
        }

        public FetchTableInfoOutput FetchTableInfo(FetchTableInfoInput input)
        {
            var output = new FetchTableInfoOutput();
            DataModels.SearchTableInfo value;
            RepositoryValue.TryGetValue(input.TableId, out value);
            if (value == null) return null;
            output.TableInfo = new FetchTableDetailInfo(value.Id, value.TableDisplayName, value.TableName);
            return output;
        }
    }
}