using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stepeco.Core.DAL.Repository.Interface
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class
    {
        private static object DbLock = new object();
        public EntityRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #region Properties
        public DbContext DbContext { get; }
        protected readonly DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DbSet
        {
            get
            {
                lock (DbLock)
                {
                    return _dbSet;
                }
            }
        }
        #endregion

        #region Methods
        public TEntity Create(TEntity entity, bool autoSave)
        {
            _dbSet.Add(entity);

            DbContext.Entry(entity).State = EntityState.Added;

            if (autoSave)
            {
                lock (DbLock)
                {
                    DbContext.SaveChanges();
                }
            }
            return entity;
        }

        public TEntity Update(TEntity entity, bool autoSave)
        {
            _dbSet.Attach(entity);

            DbContext.Entry(entity).State = EntityState.Modified;

            if (autoSave)
            {
                lock (DbLock)
                {
                    DbContext.SaveChanges();
                }
            }
            return entity;
        }

        public void Delete(TEntity entity, bool autoSave)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;
            if (autoSave)
            {
                lock (DbLock)
                {
                    DbContext.SaveChanges();
                }
            }
        }
        #endregion
    }
}
