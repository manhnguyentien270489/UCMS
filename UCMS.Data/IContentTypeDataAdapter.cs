using System.Collections.Generic;
using UCMS.Model;

namespace UCMS.Data
{
    public interface IContentTypeDataAdapter
    {
        ContentType Insert(ContentType type);
        void Update(ContentType type);
        void Delete(string id);
        List<ContentType> Select(string name);
        ContentType GetById(string id);
    }
}
