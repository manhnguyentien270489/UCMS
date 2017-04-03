using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Model;

namespace UCMS.Data
{
    public interface IWfStepDataAdapter
    {
        WFProcessStep Insert(WFProcessStep list);
        void Update(WFProcessStep list);
        void Delete(string id);
        WFProcessStep GetById(string id);
        List<WFProcessStep> Select(string stepId);
    }
}
