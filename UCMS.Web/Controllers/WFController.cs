using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.Web.Controllers
{
    public class WFController : Controller
    {
        // GET: WF
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Detail(string id)
        {
            return View();
        }
    }
}