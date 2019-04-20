﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetSearch.Models.Search
{
    public interface IFetchTableInfoRepository
    {

        FetchAllTableInfoOutput FetchAllTableInfo(FetchAllTableInfoInput intput);

        FetchTableColumnInfoOutput FetchTableColumnInfo(FetchTableColumnInfoInput input);

        FetchTableInfoOutput FetchTableInfo(FetchTableInfoInput input);

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



    public class FetchTableColumnInfoInput
    {
        public int Id { get; set; }
    }

    public class FetchTableColumnInfoOutput
    {
        public IEnumerable<FetchTableColumnDetailInfo> FetchTableColumnDetails { get; set; }
    }

    public class FetchTableColumnDetailInfo
    {

        public int Id { get; set; }

        public string ColumnName { get; set; }

        public string ColumnDisplayName { get; set; }

    }


}
