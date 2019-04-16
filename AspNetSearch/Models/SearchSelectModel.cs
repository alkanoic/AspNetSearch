using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetSearch.Models
{
    public class SearchSelectModel
    {

        public string SelectColumn { get; set; }

        public SelectValueEnum SelectValue { get; set; }

        public SearchSelectModel(string column, SelectValueEnum value)
        {
            SelectColumn = column;
            SelectValue = value;
        }

        public string ToSelectSql()
        {
            switch (SelectValue)
            {
                case SelectValueEnum.Raw:
                    return SelectColumn;

                case SelectValueEnum.Sum:
                    return $"SUM({SelectColumn})";

                case SelectValueEnum.Avg:
                    return $"AVG({SelectColumn})";
            }

            return SelectColumn;
        }

    }

    public enum SelectValueEnum
    {
        Raw,
        Sum,
        Avg
    }

}