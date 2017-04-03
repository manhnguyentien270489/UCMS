using System.Collections.Generic;
using UCMS.Data;
using UCMS.Data.DataAdapter.Postgre;
using UCMS.Model;

namespace UCMS.Services
{
    public class DataListService
    {
        private readonly IDataListDataAdapter _adapter;
        public DataListService()
        {
            _adapter = new PgDataListDataAdapter();
        }

        public DataList Create(DataList list)
        {
            return _adapter.Insert(list);
        }

        public void Update(DataList list)
        {
            _adapter.Update(list);
        }

        public void Delete(string id)
        {
            _adapter.Delete(id);
        }

        public DataList GetById(string id)
        {
            return _adapter.GetById(id);
        }

        public List<DataList> Select(string typeId)
        {
            return _adapter.Select(typeId);
        } 
    }
}
