using System.Collections.Generic;
using UCMS.Data;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class ContentService
    {
        private readonly IContentDataAdapter _adapter = new PgContentAD();

        public Content Create(Content content)
        {
            return _adapter.Insert(content);
        }

        public void Update(Content content)
        {
            _adapter.Update(content);
        }

        public void Delete(string id)
        {
            _adapter.Delete(id);
        }

        public Content GetById(string id)
        {
            return _adapter.GetById(id);
        }

        public List<Content> GetContents(string folderId)
        {
            return _adapter.GetContents(folderId);
        }

        public void InsertAttachment(Attachment attachment, string baseDir)
        {
            _adapter.InsertAttachment(attachment);
        }

        public void DeleteAttachment(string id)
        {
            _adapter.DeleteAttachment(id);
        }
    }
}
