using Emreraslan.Core.Entities.BaseEntity;
using System.Linq.Expressions;

namespace Emreraslan.DataAccess.Repos.Abstract
{
    public interface IGenericRepo<TEntity>
        where TEntity : IEntity
    {

        List<TEntity> GetAll(Expression<Func<TEntity,bool>>? filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        int Insert(TEntity entity);

    }
}
