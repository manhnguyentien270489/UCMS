using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UCMS.Model;
using UCMS.Services;

namespace UCMS.Web.APIControllers
{
    public class DataListController : ApiController
    {
        [AcceptVerbs("POST")]
        public DataList Create(DataList list)
        {
            var svc = new DataListService();
            return svc.Create(list);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(DataList list)
        {
            var svc = new DataListService();
            svc.Update(list);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            var svc = new DataListService();
            svc.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [AcceptVerbs("GET")]
        public DataList GetById(string id)
        {
            var svc = new DataListService();
            return svc.GetById(id);
        }

        [AcceptVerbs("GET")]
        public List<DataList> Select(string typeId)
        {
            var svc = new DataListService();
            return svc.Select(typeId);
        } 
    }
}