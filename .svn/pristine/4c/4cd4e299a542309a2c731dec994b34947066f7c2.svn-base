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
    public class PgWfDataAdapter : IWfDataAdapter
    {
        private string _tableName = "Workflow";
        public void Delete(string id)
        {
            var cmd = $"DELETE FROM {_tableName} WHERE Id = '{id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public WFProcess GetById(string id)
        {
            var cmd = $"SELECT * FROM {_tableName} WHERE Id = '{id}'";
            var list = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadWfProcesses);
            if (list.Count == 0) return null;
            var wfProcess = list[0];

            PgWfStepDataAdapter stepAdapter = new PgWfStepDataAdapter();
            wfProcess.Steps = stepAdapter.Select(wfProcess.Id);

            List<WFProcessRoute> routes = new List<WFProcessRoute>();
            foreach (WFProcessStep step in wfProcess.Steps)
            {
                if (step.Routes != null)
                {
                    routes.AddRange(step.Routes);
                }
            }
            wfProcess.Routes = routes;
            return wfProcess;
        }

        public WFProcess Insert(WFProcess list)
        {
            list.Id = IdHelper.Generate();
            var cmd = $"INSERT INTO {_tableName}(Id, Name, Description) VALUES ('{list.Id}','{list.Name}','{list.Description}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
            return list;
        }

        public List<WFProcess> Select()
        {
            var cmd = $"SELECT * FROM {_tableName}";
            var list = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadWfProcesses);
            return list;
        }

        public void Update(WFProcess list)
        {
            var cmd = $"UPDATE {_tableName} SET Name='{list.Name}', Description='{list.Description}' WHERE Id='{list.Id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }
    }
}
