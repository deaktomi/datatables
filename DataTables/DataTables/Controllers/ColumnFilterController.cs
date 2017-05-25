using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTables.Models.Repository;

namespace DataTables.Controllers
{
    public class ColumnFilterController : Controller
    {
        // GET: ColumnFilter
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DataProviderAction(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var nameFilter = Convert.ToString(Request["sSearch_1"]);
            var townFilter = Convert.ToString(Request["sSearch_2"]);
            var dateFilter = Convert.ToString(Request["sSearch_3"]);

            var fromID = 0;
            var toID = 0;
            if (idFilter.Contains('~'))
            {
                //Split number range filters with ~
                fromID = idFilter.Split('~')[0] == "" ? 0 : Convert.ToInt32(idFilter.Split('~')[0]);
                toID = idFilter.Split('~')[1] == "" ? 0 : Convert.ToInt32(idFilter.Split('~')[1]);
            }
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;
            if (dateFilter.Contains('~'))
            {
                //Split date range filters with ~
                fromDate = dateFilter.Split('~')[0] == "" ? DateTime.MinValue : Convert.ToDateTime(dateFilter.Split('~')[0]);
                toDate = dateFilter.Split('~')[1] == "" ? DateTime.MaxValue : Convert.ToDateTime(dateFilter.Split('~')[1]);
            }

            var filteredCompanies = DataRepository.GetCompanies()
                .Where(c => (fromID == 0 || fromID < c.ID)
                            &&
                            (toID == 0 || c.ID < toID)
                            &&
                            (nameFilter == "" || c.Name.ToLower().Contains(nameFilter.ToLower()))
                            &&
                            (townFilter == "" || c.Town == townFilter)
                            &&
                            (fromDate == DateTime.MinValue || fromDate < c.DateCreated)
                            &&
                            (toDate == DateTime.MaxValue || c.DateCreated < toDate)
                );

            //Extract only current page
            var displayedCompanies = filteredCompanies.Skip(iDisplayStart).Take(iDisplayLength);
            var result = from c in displayedCompanies
                select new[] {
                    Convert.ToString(c.ID),
                    c.Name,
                    c.Town,
                    c.DateCreated.ToShortDateString()
                };
            return Json(new
                {
                    sEcho = sEcho,
                    iTotalRecords = DataRepository.GetCompanies().Count(),
                    iTotalDisplayRecords = filteredCompanies.Count(),
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);

        }
    }
}