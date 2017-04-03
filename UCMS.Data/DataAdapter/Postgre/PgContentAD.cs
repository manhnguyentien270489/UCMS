using System.Collections.Generic;
using System.Data;
using System.Linq;
using UCMS.Core;
using UCMS.Model;

namespace UCMS.Data.DataAdapter.Postgre
{
    public class PgContentAD : IContentDataAdapter
    {
        public Content Insert(Content content)
        {
            content.Id = IdHelper.Generate();
            if (content.Tags == null) content.Tags = new List<string>();
            var tags = "{" + string.Join(",", content.Tags) + "}";
            var cmd =
                $"INSERT INTO Content(Id, Name, ContentTypeId, Values, FolderId, Tags) " +
                $" VALUES ('{content.Id}', '{content.Name}', '{content.ContentType.Id}', '{content.Values.SerializeToJson()}', '{content.Folder.Id}', '{tags}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            return content;
        }

        public void Update(Content content)
        {
            var tags = "{" + string.Join(",", content.Tags) + "}";
            var cmd = $"UPDATE Content SET Name='{content.Name}', Values='{content.Values.SerializeToJson()}', Tags='{tags}' WHERE Id='{content.Id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public void Delete(string id)
        {
            var cmd = $"DELETE FROM Content WHERE Id='{id}';";
            cmd += $"DELETE FROM Attachment WHERE ContentId ='{id}';"; 
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public Content GetById(string id)
        {
            var cmd = $"SELECT * FROM Content WHERE Id='{id}'";
            var content =
                PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                    DataHelper.ReadContents).FirstOrDefault();
            if (content != null)
            {
                cmd = $"SELECT * FROM Attachment WHERE ContentId='{id}'";
                content.Attachments = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd,
                    null, DataHelper.ReadAttachments);
                var folderAd = new PgFolderAD();
                content.Folder = folderAd.GetById(content.Folder.Id);
                var contentTypeAD = new PgContentTypeDataAdapter();
                content.ContentType = contentTypeAD.GetById(content.ContentType.Id);
            }
            return content;
        }

        public List<Content> GetContents(string folderId)
        {
            var cmd = $"SELECT * FROM Content WHERE FolderId='{folderId}'";
            var list = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                DataHelper.ReadContents);
            foreach (var content in list)
            {
                cmd = $"SELECT * FROM Attachment WHERE ContentId='{content.Id}'";
                content.Attachments = PgSqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd,
                    null, DataHelper.ReadAttachments);
            }
            return list;
        }

        public void InsertAttachment(Attachment attachment)
        {
            var cmd =
                $"INSERT INTO Attachment (Id, Name, MIME, ContentId) VALUES ('{attachment.Id}', '{attachment.Name}', '{attachment.MIME}', '{attachment.ContentId}')";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }

        public void DeleteAttachment(string id)
        {
            var cmd = $"DELETE FROM Attachment WHERE Id='{id}'";
            PgSqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);
        }
    }
}
