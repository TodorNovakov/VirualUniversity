using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityLibrary;

namespace UniversityMVC.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
        internal UniversityLibrary.SchoolEntities context;
        internal DbSet<T> dbSet;

        public Repository(UniversityLibrary.SchoolEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            var allElements = this.dbSet.ToList();

            return allElements;
        }

        public T GetById(object id)
        {
            var elemById = this.dbSet.Find(id);

            return elemById;
        }


        public void Insert(T elemT)
        {
            this.dbSet.Add(elemT);
        }

        public virtual void Delete(object id)
        {
            T elemToDelete = dbSet.Find(id);
            Delete(elemToDelete);
        }

        public virtual void Delete(T elemToDelete)
        {
            if (context.Entry(elemToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(elemToDelete);
            }
            dbSet.Remove(elemToDelete);
        }

        public virtual void Update(T elemToUpdate)
        {
            dbSet.Attach(elemToUpdate);
            context.Entry(elemToUpdate).State = EntityState.Modified;
        }
    }
}