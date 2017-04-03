using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class WFRouteService
    {
        public WFProcessRoute Create(WFProcessRoute wf)
        {
            PgWfRouteDataAdapter adapter = new PgWfRouteDataAdapter();
            return adapter.Insert(wf);
        }

        public void Update(WFProcessRoute wf)
        {
            PgWfRouteDataAdapter adapter = new PgWfRouteDataAdapter();
            adapter.Update(wf);
        }

        public void Delete(WFProcessRoute wf)
        {
            PgWfRouteDataAdapter adapter = new PgWfRouteDataAdapter();
            adapter.Delete(wf.Id);
        }
    }
}
