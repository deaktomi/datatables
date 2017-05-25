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
            IEnumerable<Company> filteredCompanies;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredCompanies = DataRepository.GetCompanies()
                    .Where(c => c.Name.Contains(param.sSearch)
                                ||
                                c.Address.Contains(param.sSearch)
                                ||
                                c.Town.Contains(param.sSearch));
            }
            else
            {
                filteredCompanies = allCompanies;
            }

            var displayedCompanies = filteredCompanies;
            var result = from c in displayedCompanies
                select new[] { Convert.ToString(c.ID), c.Name, c.Address, c.Town };
            return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = allCompanies.Count(),
                    iTotalDisplayRecords = filteredCompanies.Count(),
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}