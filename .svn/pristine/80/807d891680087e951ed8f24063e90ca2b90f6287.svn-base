using System.Collections.Generic;
using UCMS.Model;

namespace UCMS.Data
{
    public interface IContentDataAdapter
    {
        Content Insert(Content content);
        void Update(Content content);
        void Delete(string id);
        Content GetById(string id);
        List<Content> GetContents(string folderId);
        void InsertAttachment(Attachment attachment);
        void DeleteAttachment(string id);
    }
}
