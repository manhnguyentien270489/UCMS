using System.Web.Mvc;

namespace UCMS.Web.Controllers
{
    public class DataListController : Controller
    {
        // GET: DataList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEdit()
        {
            return View("~/Views/Content/AddEdit.cshtml");
        }
    }
}