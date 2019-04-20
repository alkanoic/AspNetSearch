using AspNetSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetSearch.Controllers
{
    public class SearchController : Controller
    {

        private readonly Models.Search.IFetchTableInfoRepository fetchTableInfoRepository;

        public SearchController(Models.Search.IFetchTableInfoRepository fetchTableInfoRepository)
        {
            this.fetchTableInfoRepository = fetchTableInfoRepository;
        }


        // GET: Search
        public ActionResult Index()
        {
            var vm = new SearchIndexViewModel();
            var input = new Models.Search.FetchAllTableInfoInput();
            var output = fetchTableInfoRepository.FetchAllTableInfo(input);
            var list = new List<SearchIndexDetail>();
            foreach(var item in output.FetchTableDetails)
            {
                var i = new SearchIndexDetail();
                i.TableId = item.TableId;
                i.TableDisplayName = item.TableDisplayName;
                list.Add(i);
            }
            vm.Details = list;
            return View(vm);
        }

        public ActionResult Search(int? tableId)
        {
            if (tableId.HasValue == false) return RedirectToAction("Index");

            var vm = new SearchViewModel(tableId.Value);
            var list = new List<SearchTableColumn>();

            var input = new Models.Search.FetchTableAllColumnInfoInput();
            input.TableId = tableId.Value;
            var output = fetchTableInfoRepository.FetchTableAllColumnInfo(input);

            foreach(var item in output.FetchTableColumnDetails)
            {
                var i = new SearchTableColumn();
                i.ColumnId = item.ColumnId;
                i.ColumnName = item.ColumnName;
                i.ColumnDisplayName = item.ColumnDisplayName;
                list.Add(i);
            }

            vm.Columns = list;
            return View(vm);
        }

        private static int id = 0;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxWhereList(int tableId, int searchWhereColumnId)
        {
            var vm = new SearchWhereControlViewModel();
            vm.SearchId = id++;
            var input = new Models.Search.FetchTableColumnInfoInput();
            input.ColumnId = searchWhereColumnId;
            var output = fetchTableInfoRepository.FetchTableColumnInfo(input);
            vm.SearchName = output.ColumnInfo.ColumnName;
            vm.SearchDisplayName = output.ColumnInfo.ColumnDisplayName;
            return PartialView("_SearchWhereControl", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxGroupList(int tableId, int searchGroupColumnId)
        {
            var vm = new SearchGroupControlViewModel();
            vm.SearchId = id++;
            var input = new Models.Search.FetchTableColumnInfoInput();
            input.ColumnId = searchGroupColumnId;
            var output = fetchTableInfoRepository.FetchTableColumnInfo(input);
            vm.SearchGroupName = output.ColumnInfo.ColumnName;
            vm.SearchDisplayName = output.ColumnInfo.ColumnDisplayName;
            return PartialView("_SearchGroupControl", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxSelectList(int tableId, int searchSelectColumnId)
        {
            var vm = new SearchSelectControlViewModel();
            vm.SearchId = id++;
            var input = new Models.Search.FetchTableColumnInfoInput();
            input.ColumnId = searchSelectColumnId;
            var output = fetchTableInfoRepository.FetchTableColumnInfo(input);
            vm.SearchSelectName = output.ColumnInfo.ColumnName;
            vm.SearchDisplayName = output.ColumnInfo.ColumnDisplayName;
            return PartialView("_SearchSelectControl", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPost(int tableId,
            string[] SearchWhereName,
            string[] SearchValue,
            string[] SearchRange,
            string[] SearchGroupName,
            string[] SearchSelectName,
            string[] SearchSelectValue)
        {
            var vm = new ViewModels.SearchQueryViewModel();

            if (SearchSelectName == null)
            {
                vm.Query = "Selectは1つ以上指定してください。";
                return PartialView("_SearchQuery", vm);
            }

            var tableInfoInput = new Models.Search.FetchTableInfoInput();
            tableInfoInput.TableId = tableId;
            var tableInfo = fetchTableInfoRepository.FetchTableInfo(tableInfoInput);
            if (tableInfo == null)
            {
                vm.Query = "選択されたテーブルがありません。テーブル選択からやり直してください。";
                return PartialView("_SearchQuery", vm);
            }

            var model = new Models.SearchModel();

            if (SearchWhereName != null)
            {
                model.WhereModel = new List<Models.SearchWhereModel>();
                for (var i = 0; i <= SearchWhereName.Length - 1; i++)
                {
                    var sm = new Models.SearchWhereModel(SearchWhereName[i], (Models.WhereRangeEnum)int.Parse(SearchRange[i]), SearchValue[i]);
                    model.WhereModel.Add(sm);
                }
            }

            if (SearchGroupName != null)
            {
                model.GroupModel = new List<Models.SearchGroupModel>();
                for (var i = 0; i <= SearchGroupName.Length - 1; i++)
                {
                    var sm = new Models.SearchGroupModel(SearchGroupName[i]);
                    model.GroupModel.Add(sm);
                }
            }

            if (SearchSelectName != null)
            {
                model.SelectModel = new List<Models.SearchSelectModel>();
                for (var i = 0; i <= SearchSelectName.Length - 1; i++)
                {
                    var sm = new Models.SearchSelectModel(SearchSelectName[i], (Models.SelectValueEnum)int.Parse(SearchSelectValue[i]));
                    model.SelectModel.Add(sm);
                }
            }

            var query = new DbExtensions.SqlBuilder();
            foreach (var item in model.SelectModel)
            {
                query.SELECT(item.ToSelectSql());
            }
            query.FROM(tableInfo.TableInfo.TableName);
            if (model.WhereModel != null)
            {
                foreach (var item in model.WhereModel)
                {
                    query.WHERE(item.ToWhereSql(), item.WhereValue);
                }
            }
            if (model.GroupModel != null)
            {
                foreach (var item in model.GroupModel)
                {
                    query.GROUP_BY(item.GroupColumn);
                }
            }

            vm.Query = query.ToString();
            return PartialView("_SearchQuery", vm);
        }

    }
}