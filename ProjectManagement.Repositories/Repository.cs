using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace ProjecManagement.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        private ProjecManagementDbContext context;

        private DbSet<Entity> dbSet;

        public Repository()
        {
            context = new ProjecManagementDbContext();
            dbSet = context.Set<Entity>();
        }
        public IEnumerable<Entity> GetAllRecord()
        {
            return dbSet.ToList();
        }
        public Entity GetRecordById(object id)
        {
            return dbSet.Find(id);
        }
        public Entity InsertRecord(Entity entity)
        {
            dbSet.Add(entity);
            SaveRecord();
            return entity;
        }
        public void DeleteRecord(object id)
        {
            Entity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            SaveRecord();
        }
        public void Delete(Entity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);

            }
            dbSet.Remove(entityToDelete);
        }
        public Entity UpdateRecord(Entity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            SaveRecord();
            return entity;
        }
        public void SaveRecord()
        {
            try
            {
                context.SaveChanges();
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    }
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
