using VacinaFacil.Entity.Entities;

namespace VacinaFacil.Repository.Interface.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> getByID(object id);

        Task<List<TEntity>> All();

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);

        Task DeleteById(object id);
    }
}
