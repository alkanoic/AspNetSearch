using AspNetSearch.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AspNetSearch.Controllers
{
    public class SearchController : Controller
    {

        private readonly Models.Search.IFetchTableInfoRepository fetchTableInfoRepository;

        private readonly Models.Search.ISaveSearchSettingRepository saveSearchSettingRepository;

        public SearchController(Models.Search.IFetchTableInfoRepository fetchTableInfoRepository,
            Models.Search.ISaveSearchSettingRepository saveSearchSettingRepository)
        {
            this.fetchTableInfoRepository = fetchTableInfoRepository;
            this.saveSearchSettingRepository = saveSearchSettingRepository;
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

            var saveOutput = saveSearchSettingRepository.FetchAllSetting();
            var saveList = new List<SaveSearchSettingDetail>();
            foreach(var item in saveOutput.Details)
            {
                var i = new SaveSearchSettingDetail()
                {
                    SearchSettingId = item.SearchSettingId,
                    SearchSettingName = item.SearchSettingName,
                    TableId = item.TableId
                };
                saveList.Add(i);
            }
            vm.SaveDetails = saveList;
            return View(vm);
        }

        public ActionResult Search(int? tableId, int? settingId)
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

            var groups = new List<SearchGroupControlViewModel>();
            var wheres = new List<SearchWhereControlViewModel>();
            var selects = new List<SearchSelectControlViewModel>();
            if (settingId.HasValue)
            {
                var saveInput = new Models.Search.FetchSettingInput();
                saveInput.SearchSettingId = settingId.Value;
                var saveOutput = saveSearchSettingRepository.FetchSetting(saveInput);
                foreach(var item in saveOutput.Groups)
                {
                    var group = new SearchGroupControlViewModel()
                    {
                        SearchId = id++,
                        SearchDisplayName = item.GroupColumnDisplayName,
                        SearchGroupName = item.GroupColumnName
                    };
                    groups.Add(group);
                }

                foreach(var item in saveOutput.Wheres)
                {
                    var where = new SearchWhereControlViewModel()
                    {
                        SearchId = id++,
                        SearchDisplayName = item.WhereColumnDisplayName,
                        SearchWhereName = item.WhereColumnName,
                        SearchRange = (int)item.WhereRange,
                        SearchValue = item.WhereValue
                    };
                    wheres.Add(where);
                }

                foreach(var item in saveOutput.Selects)
                {
                    var select = new SearchSelectControlViewModel()
                    {
                        SearchId = id++,
                        SearchDisplayName = item.SelectColumnDisplayName,
                        SearchSelectName = item.SelectColumnName,
                        SearchSelectValue = (int)item.SearchSelectValue
                    };
                    selects.Add(select);
                }
            }
            vm.GroupDetails = groups;
            vm.WhereDetails = wheres;
            vm.SelectDetails = selects;

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
            vm.SearchWhereName = output.ColumnInfo.ColumnName;
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
        public ActionResult SearchPost(string Search, string Save, string saveName, int tableId,
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

            model.WhereModel = new List<Models.SearchWhereModel>();
            if (SearchWhereName != null)
            {
                for (var i = 0; i <= SearchWhereName.Length - 1; i++)
                {
                    var sm = new Models.SearchWhereModel(SearchWhereName[i], (Models.WhereRangeEnum)int.Parse(SearchRange[i]), SearchValue[i]);
                    model.WhereModel.Add(sm);
                }
            }

            model.GroupModel = new List<Models.SearchGroupModel>();
            if (SearchGroupName != null)
            {
                for (var i = 0; i <= SearchGroupName.Length - 1; i++)
                {
                    var sm = new Models.SearchGroupModel(SearchGroupName[i]);
                    model.GroupModel.Add(sm);
                }
            }

            model.SelectModel = new List<Models.SearchSelectModel>();
            if (SearchSelectName != null)
            {
                for (var i = 0; i <= SearchSelectName.Length - 1; i++)
                {
                    var sm = new Models.SearchSelectModel(SearchSelectName[i], (Models.SelectValueEnum)int.Parse(SearchSelectValue[i]));
                    model.SelectModel.Add(sm);
                }
            }

            if (Save != null)
            {
                var tableInput = new Models.Search.FetchTableAllColumnInfoInput();
                tableInput.TableId = tableId;
                var tableOutput = fetchTableInfoRepository.FetchTableAllColumnInfo(tableInput);

                var input = new Models.Search.SaveSearchSettingInput();
                input.SearchSettingName = saveName;
                input.SearchTableId = tableId;
                var groups = new List<Models.Search.SaveSearchGroupInput>();
                var wheres = new List<Models.Search.SaveSearchWhereInput>();
                var selects = new List<Models.Search.SaveSearchSelectInput>();

                foreach(var item in model.GroupModel)
                {
                    var group = new Models.Search.SaveSearchGroupInput();
                    var r = tableOutput.FetchTableColumnDetails.Where(x => x.ColumnName == item.GroupColumn).SingleOrDefault();
                    group.GroupColumnId = r.ColumnId;
                    groups.Add(group);
                }

                foreach(var item in model.SelectModel)
                {
                    var select = new Models.Search.SaveSearchSelectInput();
                    var r = tableOutput.FetchTableColumnDetails.Where(x => x.ColumnName == item.SelectColumn).SingleOrDefault();
                    select.SelectColumnId = r.ColumnId;
                    select.SearchSelectValue = item.SelectValue;
                    selects.Add(select);
                }

                foreach(var item in model.WhereModel)
                {
                    var where = new Models.Search.SaveSearchWhereInput();
                    var r = tableOutput.FetchTableColumnDetails.Where(x => x.ColumnName == item.WhereColumn).SingleOrDefault();
                    where.WhereColumnId = r.ColumnId;
                    where.WhereRange = item.WhereRange;
                    where.WhereValue = item.WhereValue;
                    wheres.Add(where);
                }

                input.Groups = groups;
                input.Wheres = wheres;
                input.Selects = selects;

                saveSearchSettingRepository.SaveSearchSetting(input);
                var messageVm = new ViewModels.MessageViewModel();
                messageVm.Message = "Success";
                return PartialView("_Message", messageVm);
            }
            else
            {
                //Search

                var query = new DbExtensions.SqlBuilder();
                foreach (var item in model.SelectModel)
                {
                    query.SELECT(item.ToSelectSql());
                }
                query.FROM(tableInfo.TableInfo.TableName);
                foreach (var item in model.WhereModel)
                {
                    query.WHERE(item.ToWhereSql(), item.WhereValue);
                }
                foreach (var item in model.GroupModel)
                {
                    query.GROUP_BY(item.GroupColumn);
                }

                vm.Query = query.ToString();
                return PartialView("_SearchQuery", vm);

            }

        }
    }
}