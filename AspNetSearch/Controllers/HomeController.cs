using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AspNetSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            var vm = new AspNetSearch.ViewModels.SearchViewModel();
            vm.Controls = new List<AspNetSearch.ViewModels.SearchControlViewModel>();
            return View(vm);
        }

        private static int id = 0;

        [HttpPost]
        public ActionResult AjaxList(string searchWhereName)
        {
            var vm = new AspNetSearch.ViewModels.SearchControlViewModel();
            vm.SearchId = id++;
            vm.SearchName = searchWhereName;
            return PartialView("_SearchControl", vm);
        }

        [HttpPost]
        public ActionResult AjaxGroupList(string searchGroupName)
        {
            var vm = new AspNetSearch.ViewModels.SearchGroupControlViewModel();
            vm.SearchId = id++;
            vm.SearchGroupName = searchGroupName;
            return PartialView("_SearchGroupControl", vm);
        }

        [HttpPost]
        public ActionResult AjaxSelectList(string searchSelectName)
        {
            var vm = new AspNetSearch.ViewModels.SearchSelectControlViewModel();
            vm.SearchId = id++;
            vm.SearchSelectName = searchSelectName;
            return PartialView("_SearchSelectControl", vm);
        }

        [HttpPost]
        public ActionResult SearchPost(string[] SearchName, string[] SearchValue, string[] SearchRange, string[] SearchGroupName, string[] SearchSelectName, string[] SearchSelectValue)
        {
            var model = new Models.SearchModel();

            if(SearchName != null)
            {
                model.WhereModel = new List<Models.SearchWhereModel>();
                for (var i = 0; i <= SearchName.Length - 1; i++)
                {
                    var sm = new Models.SearchWhereModel(SearchName[i], int.Parse(SearchRange[i]), SearchValue[i]);
                    model.WhereModel.Add(sm);
                }
            }

            if(SearchGroupName != null)
            {
                model.GroupModel = new List<Models.SearchGroupModel>();
                for (var i = 0; i <= SearchGroupName.Length - 1; i++)
                {
                    var sm = new Models.SearchGroupModel(SearchGroupName[i]);
                    model.GroupModel.Add(sm);
                }
            }

            if(SearchSelectName != null)
            {
                model.SelectModel = new List<Models.SearchSelectModel>();
                for (var i = 0; i <= SearchSelectName.Length -1; i++)
                {
                    var sm = new Models.SearchSelectModel(SearchSelectName[i], int.Parse(SearchSelectValue[i]));
                    model.SelectModel.Add(sm);
                }
            }

            var query = new DbExtensions.SqlBuilder();
            foreach(var item in model.SelectModel)
            {
                query.SELECT(item.SelectColumn);
            }
            query.FROM("Log4Net");
            foreach(var item in model.WhereModel)
            {
                query.WHERE(item.WhereColumn);
            }
            foreach(var item in model.GroupModel)
            {
                query.GROUP_BY(item.GroupColumn);
            }

            return Redirect("Search");
        }

    }
}