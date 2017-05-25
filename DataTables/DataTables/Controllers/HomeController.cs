using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTables.Models;
using DataTables.Models.Repository;

namespace DataTables.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var allCompanies = DataRepository.GetCompanies();

            var result = from c in allCompanies
                select new[] { c.Name, c.Address, c.Town };

            return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = allCompanies.Count(),
                    iTotalDisplayRecords = allCompanies.Count(),
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}