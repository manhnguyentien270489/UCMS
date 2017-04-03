using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UCCM.Model;
using UCCM.Services;

namespace UCCM.Web.APIControllers
{
    public class ObjectController : ApiController
    {
        [AcceptVerbs("GET")]
        public ContentType Describe(string name)
        {
            var svc = new ObjectService(name);
            return svc.Describe();
        }

        [AcceptVerbs("POST")]
        public string Insert(string name, ObjectItem item)
        {
            var svc = new ObjectService(name);
            return svc.InsertItem(item);
        }

        [AcceptVerbs("POST")]
        public PaginationResult<ObjectItem> Query(string name, CObjectQuery q)
        {
            var svc = new ObjectService(name);
            return svc.Query(q);
        } 
    }
}
