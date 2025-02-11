using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;    

using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        internal CinemaDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(CinemaDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> InsertAsync(TEntity entity)
        {
            var addedEntity = await dbSet.AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }




        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            
            
            var updatedEntity = dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            return updatedEntity.Entity;
        }

        public async Task<TEntity?> DeleteAsync(int id)
        {
            TEntity? entity = dbSet.Find(id);
            
            if (entity != null)
            {
                var deletingEntity = await DeleteAsync(entity);
                return deletingEntity;
            }
            return entity;
        }

        public async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            var deletedEntity = dbSet.Remove(entity);
            return deletedEntity.Entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(int[] ids)
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<TEntity?> GetFirstBySpecAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetFirstBySpecAsync(ISpecification<TEntity> specification, bool tracking)
        {
            var query = ApplySpecification(specification);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }


        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

    }
}
