using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Core;
using UCMS.Model;

namespace UCMS.Data.DataAdapter.Postgre
{
    public class PgWfStepDataAdapter : IWfStepDataAdapter
    {
        private string _tableName = "WorkflowStep";
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WFProcessStep GetById(string id)
        {
            var cmd = $"SELECT * FROM {_tableName} WHERE Id = '{id}'";
            var steps = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadWfProcessSteps);
            if (steps.Count == 0) return null;
            WFProcessStep step = steps[0];

            // get routes
            PgWfRouteDataAdapter routeAdapter = new PgWfRouteDataAdapter();
            List<WFProcessRoute> routes = routeAdapter.Select(id);
            step.Routes = routes;
            return step;
        }

        public WFProcessStep Insert(WFProcessStep step)
        {
            step.Id = IdHelper.Generate();
            var cmd = $"INSERT INTO {_tableName}(Id, Name, Description, ActivityId, Parameter,WorkflowId) VALUES ('{step.Id}','{step.Name}','{step.Description}','{step.ActivityId}','{step.Parameter}','{step.WorkflowId}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            // insert routes
            if (step.Routes != null)
            {
                PgWfRouteDataAdapter routeAdapter = new PgWfRouteDataAdapter();
                foreach (WFProcessRoute r in step.Routes)
                {
                    routeAdapter.Insert(r);
                }
            }
            return step;
        }

        public List<WFProcessStep> Select(string wfId)
        {
            var cmd = $"SELECT * FROM {_tableName} WHERE WorkflowId = '{wfId}'";
            var steps = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadWfProcessSteps);

            PgWfRouteDataAdapter routeAdapter = new PgWfRouteDataAdapter();
            foreach (WFProcessStep st in steps)
            {
                st.Routes = routeAdapter.Select(st.Id); //get routes for each steps
            }
            return steps;
        }

        public void Update(WFProcessStep step)
        {
            var cmd = $"UPDATE {_tableName} SET Name='{step.Name}', Description='{step.Description}', ActivityId = '{step.ActivityId}', Parameter='{step.Parameter}' WHERE Id='{step.Id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }
    }
}
