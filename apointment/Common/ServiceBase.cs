using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Common
{
    public abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : class
    {
        public IRepository<TEntity> _repository;

        //private IReadOnlyList<TEntity> GetEntities;

        public IRepository<TEntity> Context { get; set; }
        
        public ServiceBase(IRepository<TEntity> repository)
        {
            this._repository = repository;
            Context = repository;
        }

        

        public TEntity Get(long id)
        {
            return _repository.Get(id);
        }

        public void Add(TEntity model)
        {
            _repository.Add(model);

        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public void Delete(TEntity model)
        {
            _repository.Delete(model);
        }

        public void Edit(TEntity model)
        {
            _repository.Edit(model);
        }

        public void MarkModified(TEntity model)
        {
            _repository.MarkModified(model);
        }

        public void Save()
        {
            _repository.Save();

        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _repository.AddRange(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _repository.DeleteRange(entities);
        }

        public void EditRange(IEnumerable<TEntity> entities)
        {
            _repository.EditRange(entities);
        }

       
    }
}
