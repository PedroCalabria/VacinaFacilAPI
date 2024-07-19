using Microsoft.EntityFrameworkCore;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Repository.Interface.IRepositories;

namespace VacinaFacil.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly Context _context;
        protected virtual DbSet<TEntity> EntitySet { get; }

        public BaseRepository(Context context)
        {
            _context = context;
            EntitySet = _context.Set<TEntity>();
        }

        public Task<List<TEntity>> All()
        {
            return EntitySet.ToListAsync();
        }

        public async Task Delete(TEntity entity)
        {
            EntitySet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(object id)
        {
            var entity = await EntitySet.FindAsync(id);

            if (entity != null)
            {
                await Delete(entity);
            }
        }

        public async Task<TEntity> getByID(object id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            var entityEntry = await EntitySet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entityEntry = EntitySet.Update(entity);

            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
