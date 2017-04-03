using System.Collections.Generic;
using System.Data;
using UCMS.Core;
using UCMS.Model;

namespace UCMS.Data.DataAdapter.Postgre
{
    public class PgDataListDataAdapter : IDataListDataAdapter
    {
        public DataList Insert(DataList list)
        {
            list.Id = IdHelper.Generate();
            var cmd = $"INSERT INTO DataList(Id,Name, ContentTypeId, Values) VALUES ('{list.Id}','{list.Name}','{list.ContentType.Id}','{list.Values.SerializeToJson()}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
            return list;
        }

        public void Update(DataList list)
        {
            var cmd = $"UPDATE DataList SET Name='{list.Name}', ContentTypeId='{list.ContentType.Id}', Values='{list.Values.SerializeToJson()}' WHERE Id='{list.Id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public void Delete(string id)
        {
            var cmd = $"DELETE FROM DataList WHERE Id = '{id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public DataList GetById(string id)
        {
            var cmd = $"SELECT * FROM DataList WHERE Id = '{id}'";
            var list = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadDataList);
            if (list.Count == 0) return null;

            var adapter = new PgContentTypeDataAdapter();
            list[0].ContentType = adapter.GetById(list[0].ContentType.Id);
            return list[0];
        }

        public List<DataList> Select(string typeId)
        {
            var cmd = $"SELECT * FROM DataList WHERE ContentTypeId='{typeId}'";
            var list = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadDataList);
            var adapter = new PgContentTypeDataAdapter();
            foreach (var item in list)
            {
                item.ContentType = adapter.GetById(item.ContentType.Id);
            }
            return list;
        }
    }
}
