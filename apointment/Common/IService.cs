using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Common
{
    public interface IService<TEntity>
    {
        TEntity Get(long id);
        
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity model);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(int id);
        void Delete(TEntity model);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Edit(TEntity model);
        void EditRange(IEnumerable<TEntity> entities);

        void MarkModified(TEntity model);
        void Save();

    }
}
