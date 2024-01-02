using Emreraslan.Core.Entities.BaseEntity;
using Emreraslan.DataAccess.Repos.Abstract;
using Emreraslan.Services.Abstract;
using System.Linq.Expressions;

namespace Emreraslan.Services.Concrete
{
    public class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity : IEntity, new()
    {
        private readonly IGenericRepo<TEntity> _repo;
        public GenericService(IGenericRepo<TEntity> repo)
        {
            _repo = repo;
        }

        public void Delete(TEntity entity)
        {
            _repo.Delete(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _repo.Get(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            return _repo.GetAll(filter);
        }

        public TEntity GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int Insert(TEntity entity)
        {
            return _repo.Insert(entity);
            
        }

        public void Update(TEntity entity)
        {
            _repo.Update(entity);
        }
    }
}
