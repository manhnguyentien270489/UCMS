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
    public class MetadataService
    {
        public string CreateObject(ContentType objDef)
        {
            var id = IdHelper.Generate(25);

            var cmd = $"INSERT INTO ObjectMetadata (Id, ObjectName, Description) VALUES ('{id}', '{objDef.Name}', '{objDef.Description}')";
            cmd += Environment.NewLine;
            cmd += $"CREATE TABLE {objDef.Name} ([Id] [nvarchar](25) NOT NULL, CONSTRAINT [PK_{objDef.Name}] PRIMARY KEY CLUSTERED([Id] ASC))";

            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            return id;
        }

        public List<ContentType> GetObjects()
        {
            var cmd = "SELECT Id, ObjectName, Description FROM ObjectMetadata WITH(NOLOCK)";
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, ReadObjects);
        }        

        private List<ContentType> ReadObjects(IDataReader reader)
        {
            var list = new List<ContentType>();
            while (reader.Read())
            {
                var obj = new ContentType
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["ObjectName"].ToString(),
                    Description = reader["Description"].ToString()
                };
                list.Add(obj);
            }
            return list;
        }

        public ContentType GetById(string id)
        {
            var cmd = $"SELECT Id, ObjectName, Description FROM  ObjectMetadata WHERE Id='{id}'";
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, reader => ReadObjects(reader).FirstOrDefault());
        }

        public ContentType GetByName(string objectName)
        {
            var cmd = $"SELECT Id, ObjectName, Description FROM  ObjectMetadata WHERE ObjectName='{objectName}'";
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, reader => ReadObjects(reader).FirstOrDefault());
        }

        public void DeleteObject(string objectName)
        {
            
        }

        public string CreateField(ContentTypeField fieldDef)
        {
            var id = IdHelper.Generate(25);

            var obj = GetById(fieldDef.ContentTypeId);

            var cmd = "INSERT INTO FieldMetadata (Id, Name, DataType, Length, LookupName, Items, ContentTypeId) " +
                      $"VALUES('{id}', '{fieldDef.Name}', '{fieldDef.DataType}', '{fieldDef.Length}', '{fieldDef.LookupName}', '{string.Join("|", fieldDef.Items)}', '{fieldDef.ContentTypeId}')";
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            switch (fieldDef.DataType)
            {
                case DataType.Text:
                    cmd = $"ALTER TABLE {obj.Name} ADD [{fieldDef.Name}] [nvarchar](255)";
                    break;
                case DataType.TextArea:
                    cmd = $"ALTER TABLE {obj.Name} ADD [{fieldDef.Name}] [nvarchar](max)";
                    break;
                case DataType.Checkbox:
                    cmd = $"ALTER TABLE {obj.Name} ADD [{fieldDef.Name}] [bit]";
                    break;
                case DataType.Number:
                    cmd = $"ALTER TABLE {obj.Name} ADD [{fieldDef.Name}] [int]";
                    break;
                case DataType.DateTime:
                    cmd = $"ALTER TABLE {obj.Name} ADD [{fieldDef.Name}] [DateTime]";
                    break;
                case DataType.Picklist:
                    break;
                case DataType.MultiSelect:
                    break;
                case DataType.Lookup:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            return id;
        }

        public void UpdateField(string objectName, ContentTypeField field)
        {
        }

        public void DeleteField(string objectName, string fieldId)
        {
        }

        public List<ContentTypeField> GetObjectFields(string objId)
        {
            var cmd = $"SELECT Id, DataType, Name, Length, Items, LookupName, ObjectId FROM FieldMetadata WITH (NOLOCK) WHERE ObjectId='{objId}'";
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, ReadFields);
        }

        private List<ContentTypeField> ReadFields(IDataReader reader)
        {
            var list = new List<ContentTypeField>();
            while (reader.Read())
            {
                var field = new ContentTypeField
                {
                    Id = reader["Id"].ToString(),
                    DataType = (DataType) Enum.Parse(typeof (DataType), reader["DataType"].ToString(), true),
                    Items = reader["Items"].ToString().Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Length = Convert.ToInt32(reader["Length"]),
                    LookupName = reader["LookupName"].ToString(),
                    Name = reader["Name"].ToString(),
                    ContentTypeId = reader["ContentTypeId"].ToString()
                };
                list.Add(field);
            }
            return list;
        } 
    }
}
