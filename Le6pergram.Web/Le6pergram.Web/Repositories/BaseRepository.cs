using Le6pergram.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Le6pergram.Web.Repositories
{
    public abstract class BaseRepository<T> where T : Entity
    {
        private Le6pergramDatabase context;
        private DbSet<T> dbSet;

        public BaseRepository()
        {
            context = new Le6pergramDatabase();
            dbSet = context.Set<T>();
        }

        public void Insert(T item)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                dbSet.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(T item)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                dbSet.AddOrUpdate(i => i.Id, item);
                context.SaveChanges();
            }
        }

        public virtual T GetById(int id)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
                return dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
                return dbSet.ToList();
        }

        public virtual void Delete(T item)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                T itemToDelete = dbSet.Find(item.Id);
                dbSet.Remove(itemToDelete);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}