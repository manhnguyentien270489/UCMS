using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UCMS.Model;
using UCMS.Services;

namespace UCMS.Web.APIControllers
{
    public class FolderController : ApiController
    {
        [AcceptVerbs("GET")]
        public List<Folder> GetFolders(string parentId)
        {
            if (string.IsNullOrEmpty(parentId)) parentId = null;
            var svc = new FolderService();
            return svc.GetFolders(parentId);
        }

        [AcceptVerbs("GET")]
        public Folder GetFolder(string id)
        {
            var svc = new FolderService();
            return svc.GetFolder(id);
        }
        [AcceptVerbs("POST")]
        public Folder CreateFolder(Folder folder)
        {
            var svc = new FolderService();
            return svc.CreateFolder(folder);
        }
        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateFolder(Folder folder)
        {
            var svc = new FolderService();
            svc.UpdateFolder(folder);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeleteFolder(string id)
        {
            var svc = new FolderService();
            svc.DeleteFolder(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
