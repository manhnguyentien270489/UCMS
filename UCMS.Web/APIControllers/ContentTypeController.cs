using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UCMS.Model;
using UCMS.Services;

namespace UCMS.Web.APIControllers
{
    public class ContentTypeController : ApiController
    {
        [AcceptVerbs("GET")]
        public ContentType GetById(string id)
        {
            var svc = new ContentTypeService();
            return svc.GetById(id);
        }

        [AcceptVerbs("GET")]
        public List<ContentType> GetAll()
        {
            var svc = new ContentTypeService();
            return svc.Get();
        }

        [AcceptVerbs("POST")]
        public ContentType Create(ContentType type)
        {
            var svc = new ContentTypeService();
            return svc.Create(type);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(ContentType type)
        {
            var svc = new ContentTypeService();
            svc.Update(type);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new ContentTypeService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
