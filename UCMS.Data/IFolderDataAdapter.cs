using System.Collections.Generic;
using UCMS.Model;

namespace UCMS.Data
{
    public interface IFolderDataAdapter
    {
        Folder Insert(Folder folder);
        void Update(Folder folder);
        void Delete(string id);
        Folder GetById(string id);
        List<Folder> GetFolders(string parentId);
    }
}
