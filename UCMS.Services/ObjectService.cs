using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCM.Core;
using UCCM.Model;

namespace UCCM.Services
{
    public class ObjectService
    {
        public string ObjectName { get; set; }

        public ObjectService(string name)
        {
            ObjectName = name;
        }

        public ContentType Describe()
        {
            var svc = new MetadataService();
            var obj = svc.GetByName(ObjectName);

            obj.Fields = svc.GetObjectFields(obj.Id);

            return obj;
        }

        public string InsertItem(ObjectItem record)
        {
            var id = IdHelper.Generate(25);
            var cmd = $"INSERT INTO {ObjectName} (Id, ";
            cmd = record.Keys.Aggregate(cmd, (current, fieldName) => current + (fieldName + ","));
            cmd = cmd.TrimEnd(',');
            cmd += ") VALUES ('" + id + "',";
            cmd = record.Values.Aggregate(cmd, (current, value) => current + ("'" + value + "',"));
            cmd = cmd.TrimEnd(',');
            cmd += ")";
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
            return id;
        }

        public void UpdateItem(string id, ObjectItem record)
        {
        }

        public void DeleteItem(string id)
        {
            
        }

        public ObjectItem Get(string id)
        {
            return null;
        }

        public PaginationResult<ObjectItem> Query(CObjectQuery q)
        {
            var cmd = q.Fields.Aggregate("SELECT ", (current, f) => current + (f + ","));
            cmd = cmd.TrimEnd(',');
            cmd += " FROM " + ObjectName;
            if (!string.IsNullOrEmpty(q.Conditions))
                cmd += " WHERE " + q.Conditions;
            cmd += " ORDER BY Id OFFSET " + (q.PageIndex - 1)*q.PageSize + " ROWS";
            cmd += " FETCH NEXT " + q.PageSize + " ROWS ONLY";
            
            cmd += Environment.NewLine;
            cmd += "SELECT Count(1) FROM " + ObjectName;
            if (!string.IsNullOrEmpty(q.Conditions))
                cmd += " WHERE " + q.Conditions;

            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, reader =>
            {
                var result = new PaginationResult<ObjectItem>() {Items = new List<ObjectItem>()};

                while (reader.Read())
                {
                    var item = new ObjectItem();
                    foreach (var f in q.Fields)
                    {
                        item.Add(f, reader[f].ToString());
                    }
                    if (q.Fields.Contains("Id")) item.Id = item["Id"].ToString();
                    result.Items.Add(item);
                }
                reader.NextResult();
                reader.Read();
                result.Total = Convert.ToInt32(reader[0]);

                return result;
            });
        }
    }
}
