using Stock.Model.Base;
using Stock.Repository.Base;
using Stock.Repository.Exceptions;
using System.Collections.Generic;

namespace Stock.AppService.Base
{
    public class BaseService<TEntity>
        where TEntity : class, IEntity
    {
        protected IRepository<TEntity> Repository { get; set; }

        public TEntity Create(TEntity entity)
        {
            return this.Repository.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.Repository.GetAll();
        }

        public TEntity Get(int id)
        {
            var result = this.Repository.Get(id);
            if (result == null)
                throw new RepositoryException(string.Format("No existe la entidad ({0}) para el identificador {1}", typeof(TEntity).Name, id.ToString()));
            return result;
        }

        public void Delete(TEntity entity)
        {
            this.Repository.Delete(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return this.Repository.Update(entity);
        }
    }
}
