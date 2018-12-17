using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjecManagement.Repositories
{
    public interface IRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> GetAllRecord();
        Entity GetRecordById(object Id);
        Entity InsertRecord(Entity obj);
        void DeleteRecord(object Id);
        Entity UpdateRecord(Entity obj);
        void SaveRecord();
    }
}
