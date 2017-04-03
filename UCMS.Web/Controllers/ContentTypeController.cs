using System.Web.Mvc;

namespace UCMS.Web.Controllers
{
    public class ContentTypeController : Controller
    {
        // GET: ContentType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEdit()
        {
            return View();
        }
    }
}