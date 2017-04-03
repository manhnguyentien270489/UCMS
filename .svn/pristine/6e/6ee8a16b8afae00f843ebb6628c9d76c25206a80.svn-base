using System.Collections.Generic;
using UCMS.Core;
using UCMS.Data;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class ContentTypeService
    {
        private readonly IContentTypeDataAdapter _dataAdapter;

        public ContentTypeService()
        {
            _dataAdapter = new PgContentTypeDataAdapter();
        }

        public ContentType Create(ContentType type)
        {
            type.Id = IdHelper.Generate();
            foreach (var field in type.Fields)
            {
                field.Id = IdHelper.Generate();
                field.ContentTypeId = type.Id;
            }
            return _dataAdapter.Insert(type);
        }

        public void Update(ContentType type)
        {
            _dataAdapter.Update(type);
        }

        public void Delete(string id)
        {
            _dataAdapter.Delete(id);
        }

        public ContentType GetById(string id)
        {
            return _dataAdapter.GetById(id);
        }

        public List<ContentType> Get(string name = "")
        {
            return _dataAdapter.Select(name);
        } 
    }
}
