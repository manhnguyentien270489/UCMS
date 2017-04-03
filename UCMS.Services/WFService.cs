using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class WFService
    {
        public List<WFProcess> GetAll()
        {
            PgWfDataAdapter adapter = new PgWfDataAdapter();
            return adapter.Select();
        }

        public WFProcess GetById(string id)
        {
            PgWfDataAdapter adapter = new PgWfDataAdapter();
            return adapter.GetById(id);
        }

        public WFProcess Create(WFProcess wf)
        {
            PgWfDataAdapter adapter = new PgWfDataAdapter();
            return adapter.Insert(wf);
        }

        public void Update(WFProcess wf)
        {
            PgWfDataAdapter adapter = new PgWfDataAdapter();
            adapter.Update(wf);
        }

        public void Delete(WFProcess wf)
        {
            PgWfDataAdapter adapter = new PgWfDataAdapter();
            adapter.Delete(wf.Id);
        }
    }
}
