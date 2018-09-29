using Microsoft.EntityFrameworkCore;
using Stock.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Repository.Base
{
    public class BaseRepository<TEntity>: IRepository<TEntity>
        where TEntity: class, IEntity
    {
        protected IDbContext<TEntity> DbContext { get; set; }

        public TEntity Add(TEntity entity)
        {            
            //this.DbContext.Database.OpenConnection();
            var result = this.DbContext.Add(entity);
            this.DbContext.SaveChanges();
            //this.DbContext.Database.CloseConnection();
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            //this.DbContext.Database.OpenConnection();
            var productos = this.DbContext.GetAll();
            //this.DbContext.Database.CloseConnection();
            return productos;
        }

        public TEntity Get(int id)
        {
            //this.DbContext.Database.OpenConnection();
            var result = this.DbContext.DbSet.Find(id);
            //this.DbContext.Database.CloseConnection();
            return result;
        }

        public void Delete(TEntity entity)
        {
            //this.DbContext.Database.OpenConnection();
            this.DbContext.Remove(entity);
            this.DbContext.SaveChanges();
            //this.DbContext.Database.CloseConnection();
        }

        public TEntity Update(TEntity entity)
        {
            //this.DbContext.Database.OpenConnection();
            var result = this.DbContext.Update(entity);
            this.DbContext.SaveChanges();
            //this.DbContext.Database.CloseConnection();
            return result;
        }
    }
}
