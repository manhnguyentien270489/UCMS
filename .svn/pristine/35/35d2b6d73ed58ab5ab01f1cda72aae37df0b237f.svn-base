using System.Collections.Generic;
using System.Data;
using System.Linq;
using UCMS.Core;
using UCMS.Model;

namespace UCMS.Data.DataAdapter.Postgre
{
    public class PgFolderAD : IFolderDataAdapter
    {
        public Folder Insert(Folder folder)
        {
            folder.Id = IdHelper.Generate();
            if (string.IsNullOrEmpty(folder.ParentId))
            {
                folder.Path = new List<FolderPathItem> {new FolderPathItem {Id = folder.Id, Name = folder.Name}};
            }
            else
            {
                var parentFolder = GetById(folder.ParentId);
                folder.Path = parentFolder.Path;
                folder.Path.Add(new FolderPathItem { Id = folder.Id, Name = folder.Name });
            }
            var cmd = $"INSERT INTO Folder (Id, Name, ParentId, Path) VALUES ('{folder.Id}','{folder.Name}','{folder.ParentId}','{folder.Path.SerializeToJson()}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
            return folder;
        }

        public void Update(Folder folder)
        {
            var cmd = $"UPDATE Folder SET Name='{folder.Name}' WHERE Id='{folder.Id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            cmd = $"SELECT * FROM Folder f, jsonb_array_elements(f.Path) p WHERE p->>Id='{folder.Id}'";
            var list = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadFolders);
            foreach (var dir in list)
            {
                var item = dir.Path.Find(p => p.Id.Equals(folder.Id));
                item.Name = folder.Name;
                cmd = $"UPDATE Folder SET Path='{dir.Path.SerializeToJson()}' WHERE Id='{dir.Id}'";
                PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
            }
        }

        public void Delete(string id)
        {
            var cmd = $"DELETE FROM Folder WHERE Id IN (SELECT Id FROM Folder, jsonb_array_elements(Path) p WHERE p->>Id='{id}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public Folder GetById(string id)
        {
            var cmd = $"SELECT * FROM Folder WHERE Id='{id}'";
            return PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                reader => DataHelper.ReadFolders(reader).FirstOrDefault());
        }

        public List<Folder> GetFolders(string parentId)
        {
            var cmd = $"SELECT * FROM Folder";
            if (string.IsNullOrEmpty(parentId))
                cmd += " WHERE ParentId='NULL' OR ParentId=''";
            else
                cmd += $" WHERE ParentId='{parentId}'";
            return PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, DataHelper.ReadFolders);
        }
    }
}
