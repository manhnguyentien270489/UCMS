using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class WFStepService
    {
        public WFProcessStep Create(WFProcessStep wf)
        {
            PgWfStepDataAdapter adapter = new PgWfStepDataAdapter();
            return adapter.Insert(wf);
        }

        public void Update(WFProcessStep wf)
        {
            PgWfStepDataAdapter adapter = new PgWfStepDataAdapter();
            adapter.Update(wf);
        }

        public void Delete(WFProcessStep wf)
        {
            PgWfStepDataAdapter adapter = new PgWfStepDataAdapter();
            adapter.Delete(wf.Id);
        }
    }
}
