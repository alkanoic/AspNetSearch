using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetSearch.Models.Search
{
    public interface IFetchTableInfoRepository
    {

        FetchAllTableInfoOutput FetchAllTableInfo(FetchAllTableInfoInput intput);

        FetchTableAllColumnInfoOutput FetchTableAllColumnInfo(FetchTableAllColumnInfoInput input);

        FetchTableInfoOutput FetchTableInfo(FetchTableInfoInput input);

        FetchTableColumnInfoOutput FetchTableColumnInfo(FetchTableColumnInfoInput input);

    }

    public class FetchAllTableInfoInput
    {

    }

    public class FetchAllTableInfoOutput
    {
        public IEnumerable<FetchTableDetailInfo> FetchTableDetails { get; set; }
    }

    public class FetchTableDetailInfo
    {
        public int TableId { get; set; }

        public string TableDisplayName { get; set; }

        public string TableName { get; set; }

        public FetchTableDetailInfo(int id, string displayName, string tableName)
        {
            TableId = id;
            TableDisplayName = displayName;
            TableName = tableName;
        }

    }


    public class FetchTableInfoInput
    {
        public int TableId { get; set; }
    }

    public class FetchTableInfoOutput
    {
        public FetchTableDetailInfo TableInfo { get; set; }
    }



    public class FetchTableAllColumnInfoInput
    {
        public int TableId { get; set; }
    }

    public class FetchTableAllColumnInfoOutput
    {
        public IEnumerable<FetchTableColumnDetailInfo> FetchTableColumnDetails { get; set; }
    }

    public class FetchTableColumnDetailInfo
    {

        public int ColumnId { get; set; }

        public string ColumnName { get; set; }

        public string ColumnDisplayName { get; set; }

    }



    public class FetchTableColumnInfoInput
    {
        public int ColumnId { get; set; }
    }

    public class FetchTableColumnInfoOutput
    {
        public FetchTableColumnDetailInfo ColumnInfo { get; set; }
    }

}
