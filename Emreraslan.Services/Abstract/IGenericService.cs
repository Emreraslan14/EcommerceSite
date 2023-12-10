using Emreraslan.Core.Entities.BaseEntity;
using System.Linq.Expressions;

namespace Emreraslan.Services.Abstract
{
    public interface IGenericService<TEntity> 
        where TEntity : IEntity , new()
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        int Insert(TEntity entity);

    }
}
