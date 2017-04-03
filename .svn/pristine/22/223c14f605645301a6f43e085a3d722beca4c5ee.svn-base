using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using UCMS.Model;
using UCMS.Services;

namespace UCMS.Web.APIControllers
{
    public class WFController : ApiController
    {
        [AcceptVerbs("GET")]
        public List<WFProcess> GetWorkflows()
        {
            var svc = new WFService();
            return svc.GetAll();
        }

        [AcceptVerbs("GET")]
        public WFProcess GetWorkflowDetail(string wfId)
        {
            var svc = new WFService();
            return svc.GetById(wfId);
        }

        [AcceptVerbs("POST")]
        public WFProcess CreateWFProcess(WFProcess wf)
        {
            var svc = new WFService();
            return svc.Create(wf);
        }

        [AcceptVerbs("POST")]
        public void UpdateWFProcess(WFProcess wf)
        {
            var svc = new WFService();
            svc.Update(wf);
        }

        [AcceptVerbs("POST")]
        public WFProcessStep CreateWFStep(WFProcessStep step)
        {
            var svc = new WFStepService();
            return svc.Create(step);
        }

        [AcceptVerbs("POST")]
        public WFProcessRoute CreateWFRoute(WFProcessRoute route)
        {
            var svc = new WFRouteService();
            return svc.Create(route);
        }
    }
}