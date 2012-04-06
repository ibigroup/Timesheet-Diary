using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ibi.TimesheetDiary.Web.Mvc.Controllers
{
    using Ibi.TimesheetDiary.Data;
    using Ibi.TimesheetDiary.Data.EntityFramework;

    public class HomeController : Controller
    {
        private readonly IDataContext dataContext;

        public HomeController(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
