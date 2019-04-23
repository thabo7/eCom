using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.SQL;

namespace MyShop.DataAcess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext Context;
        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.Context = context;
            this.dbSet = context.Set<T>();

        }

        public IQueryable<T> Collection()
        {
            return dbSet;
           
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (Context.Entry(t).State == EntityState.Detached)
             dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            Context.Entry(t).State = EntityState.Modified;
        }
    }
}
