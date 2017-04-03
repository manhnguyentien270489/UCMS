using System.IO;
using System.Web;
using System.Web.Http;
using UCMS.Core;
using UCMS.Model;

namespace UCMS.Web.APIControllers
{
    public class ConfigController : ApiController
    {
        [AcceptVerbs("GET")]
        public SidebarMenu GetSidebarMenu()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/SidebarMenu.json");
            var menu = File.ReadAllText(filePath).FromJson<SidebarMenu>();
            foreach (var item in menu.Items)
            {
                var refererUrl = Request.Headers.Referrer.AbsolutePath;
                if (item.Url == null) item.Url = "/";
                if ((refererUrl == "/" && item.Url == "/") ||
                    (refererUrl.StartsWith(item.Url) && item.Url != "/"))
                {
                    item.Selected = true;
                    break;
                }
            }
            return menu;
        }
    }
}