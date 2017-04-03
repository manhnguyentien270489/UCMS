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
    public class PgWfRouteDataAdapter : IWfRouteDataAdapter
    {
        private string _tableName = "WorkflowRoute";
        public void Delete(string id)
        {
            var cmd = $"DELETE FROM {_tableName} WHERE Id = '{id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public WFProcessRoute GetById(string id)
        {
            var cmd = $"SELECT * FROM {_tableName} WHERE Id = '{id}'";
            var route = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadWfProcessRoute);
            if (route.Count == 0) return null;
            return route[0];
        }

        public WFProcessRoute Insert(WFProcessRoute route)
        {
            route.Id = IdHelper.Generate();
            var cmd = $"INSERT INTO {_tableName}(Id, FromStepId, ToStepId, RoutingParam) VALUES ('{route.Id}','{route.FromStepId}','{route.ToStepId}','{route.RoutingParam}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
            return route;
        }

        public List<WFProcessRoute> Select(string stepId)
        {
            var cmd = $"SELECT * FROM {_tableName} WHERE FromStepId = '{stepId}'";
            var route = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadWfProcessRoute);
            return route;
        }

        public void Update(WFProcessRoute route)
        {
            var cmd = $"UPDATE {_tableName} SET FromStepId='{route.FromStepId}', ToStepId='{route.ToStepId}', RoutingParam='{route.RoutingParam}' WHERE Id='{route.Id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }
    }
}
