using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using UCCM.Model;
using UCCM.Services;

namespace UCCM.Web.APIControllers
{
    public class MetadataController : ApiController
    {
        [AcceptVerbs("POST")]
        public string CreateObject(ContentType obj)
        {
            var svc = new MetadataService();
            return svc.CreateObject(obj);
        }

        [AcceptVerbs("POST")]
        public string CreateField(ContentTypeField field)
        {
            var svc = new MetadataService();
            return svc.CreateField(field);
        }

        [AcceptVerbs("GET")]
        public List<ContentType> GetObjects()
        {
            var svc = new MetadataService();
            return svc.GetObjects();
        }

        [AcceptVerbs("GET")]
        public ContentType GetObject(string id)
        {
            var svc = new MetadataService();
            var obj = svc.GetById(id);
            obj.Fields = svc.GetObjectFields(id);
            return obj;
        }
    }
}