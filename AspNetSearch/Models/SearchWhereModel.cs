using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models
{
    public class SearchWhereModel
    {

        public string WhereColumn { get; set; }

        public WhereRangeEnum WhereRange { get; set; }

        public string WhereValue { get; set; }


        public SearchWhereModel(string column, WhereRangeEnum range, string value)
        {
            WhereColumn = column;
            WhereRange = range;
            WhereValue = value;
        }

        public string ToWhereSql()
        {
            switch (WhereRange)
            {
                case WhereRangeEnum.Equal:
                    return WhereColumn + " = {0}";

                case WhereRangeEnum.Greater:
                    return WhereColumn + " > {0}";

                case WhereRangeEnum.Less:
                    return WhereColumn + " < {0}";

                case WhereRangeEnum.EqualGreater:
                    return WhereColumn + " >= {0}";

                case WhereRangeEnum.EqualLess:
                    return WhereColumn + " <= {0}";

            }
            return WhereColumn;
        }

    }

    public enum WhereRangeEnum
    {
        Equal,
        Greater,
        Less,
        EqualGreater,
        EqualLess
    }

}