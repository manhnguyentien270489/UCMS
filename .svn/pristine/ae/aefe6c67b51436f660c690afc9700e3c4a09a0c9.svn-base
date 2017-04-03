using System.Collections.Generic;
using UCMS.Data;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class FolderService
    {
        private readonly IFolderDataAdapter _adapter;
        public FolderService()
        {
            _adapter = new PgFolderAD();
        }
        public List<Folder> GetFolders(string parentId)
        {
            return _adapter.GetFolders(parentId);
        }

        public Folder GetFolder(string id)
        {
            return _adapter.GetById(id);
        }

        public Folder CreateFolder(Folder folder)
        {
            return _adapter.Insert(folder);
        }
        public void UpdateFolder(Folder folder)
        {
            _adapter.Update(folder);
        }

        public void DeleteFolder(string id)
        {
            _adapter.Delete(id);
        }
    }
}
