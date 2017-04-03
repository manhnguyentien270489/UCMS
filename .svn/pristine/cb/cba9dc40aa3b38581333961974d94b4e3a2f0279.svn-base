using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using UCMS.Core;
using UCMS.Model;

namespace UCMS.Data.DataAdapter.Postgre
{
    public class PgContentTypeDataAdapter : IContentTypeDataAdapter
    {
        public ContentType Insert(ContentType type)
        {
            PgSqlHelper.StartTransaction(ConfigManager.ConnectionString, (conn, trans) =>
            {
                var cmd = $"INSERT INTO ContentType(Id, Name, IsDataList, Description) VALUES ('{type.Id}','{type.Name}', '{type.IsDataList}', '{type.Description}')";
                PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);

                foreach (var field in type.Fields)
                {
                    InsertField(field, conn, trans);
                }
            });
            return type;
        }

        public void Update(ContentType type)
        {
            var originalType = GetById(type.Id);
            PgSqlHelper.StartTransaction(ConfigManager.ConnectionString, (conn, trans) =>
            {
                var cmd = $"UPDATE ContentType SET Name='{type.Name}', Description='{type.Description}' WHERE Id='{type.Id}'";
                PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);

                foreach (var field in type.Fields.Where(f => f.Id.StartsWith("new", StringComparison.CurrentCultureIgnoreCase)))
                {
                    field.Id = IdHelper.Generate();
                    field.ContentTypeId = type.Id;
                    InsertField(field, conn, trans);
                }

                foreach (var field in type.Fields.Where(f => originalType.Fields.Exists(of => of.Id == f.Id)))
                {
                    UpdateField(field, conn, trans);
                }

                foreach (var field in originalType.Fields.Where(of => !type.Fields.Exists(f => f.Id == of.Id)))
                {
                    DeleteField(field, conn, trans);
                }
            });
        }

        public void Delete(string id)
        {
            PgSqlHelper.StartTransaction(ConfigManager.ConnectionString, (conn, trans) =>
            {
                var cmd = $"DELETE FROM ContentType WHERE Id='{id}'";
                PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);

                cmd = $"DELETE FROM ContentTypeField WHERE ContentTypeId='{id}'";
                PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);
            });
        }

        public List<ContentType> Select(string name)
        {
            var cmd = $"SELECT Id FROM ContentType";
            if (!string.IsNullOrEmpty(name))
            {
                cmd += $" WHERE Name LIKE '%{name}%'";
            }
            return PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, reader =>
            {
                var list = new List<ContentType>();
                while (reader.Read())
                {
                    var id = reader["Id"].ToString();
                    var item = GetById(id);
                    list.Add(item);
                }
                return list;
            });
        }

        public ContentType GetById(string id)
        {
            var cmd = $"SELECT * FROM ContentType WHERE Id='{id}';" +
                      $"SELECT * FROM ContentTypeField WHERE ContentTypeId='{id}'";
            return PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, DataHelper.ReadContentType);
        }
        private void InsertField(ContentTypeField field, NpgsqlConnection conn, NpgsqlTransaction trans)
        {
            field.Id = IdHelper.Generate();
            var cmd = $"INSERT INTO ContentTypeField(Id, Name, Label, DataType, Length, LookupType,LookupField, ContentTypeId, Items) VALUES ('{field.Id}','{field.Name}', '{field.Label}', '{field.DataType}','{field.Length}','{field.LookupType}','{field.LookupField}','{field.ContentTypeId}', '{string.Join("|", field.Items)}')";
            PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);
        }

        private void UpdateField(ContentTypeField field, NpgsqlConnection conn, NpgsqlTransaction trans)
        {
            var cmd = $"UPDATE ContentTypeField SET Name='{field.Name}', Label='{field.Label}', DataType='{field.DataType}', Length='{field.Length}', LookupType='{field.LookupType}',LookupField='{field.LookupField}', ContentTypeId='{field.ContentTypeId}', Items='{string.Join("|", field.Items)}' WHERE Id='{field.Id}'";
            PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);
        }

        private void DeleteField(ContentTypeField field, NpgsqlConnection conn, NpgsqlTransaction trans)
        {
            var cmd = $"DELETE FROM ContentTypeField WHERE Id='{field.Id}'";
            PgSqlHelper.ExecuteCommand(conn, trans, CommandType.Text, cmd, null);
        }
    }
}
