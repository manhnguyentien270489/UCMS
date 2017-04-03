using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UCMS.Model;
using UCMS.Services;

namespace UCMS.Web.APIControllers
{
    public class ContentController : ApiController
    {
        [AcceptVerbs("GET")]
        public Content GetById(string id)
        {
            var svc = new ContentService();
            return svc.GetById(id);
        }

        [AcceptVerbs("GET")]
        public List<Content> GetContents(string folderId)
        {
            var svc = new ContentService();
            return svc.GetContents(folderId);
        }

        [AcceptVerbs("POST")]
        public Content Create(Content content)
        {
            var svc =new ContentService();
            return svc.Create(content);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(Content content)
        {
            var svc = new ContentService();
            svc.Update(content);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new ContentService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
