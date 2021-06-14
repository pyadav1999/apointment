using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace apointment.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);
       
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        int GetCount();
        int GetCount(Expression<Func<TEntity, bool>> predicate);

        void MarkModified(TEntity entity);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Delete(long id);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        void Edit(TEntity entity);
        void EditRange(IEnumerable<TEntity> entities);

        void Save();

        List<TEntity> GetWithRawSql(string query, params object[] parameters);
    }
}
