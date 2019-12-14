using Microsoft.EntityFrameworkCore;
using Stepeco.Core.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Stepeco.Core.BLL.Base
{
    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class
    {
        private readonly IEntityRepository<TEntity> _repository;

        public EntityService(IEntityRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<TEntity> AllAsQueryable
        {
            get { return _repository.DbSet.AsQueryable().AsNoTracking(); }
        }

        public virtual TEntity[] All
        {
            get { return AllAsQueryable.ToArray(); }
        }

        public virtual int Count
        {
            get { return AllAsQueryable.Count(); }
        }

        public virtual TEntity ByID(object id)
        {
            return _repository.DbSet.Find(id);
        }

        public virtual DbSet<TEntity> AsObjectQuery()
        {
            return _repository.DbSet;
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return AllAsQueryable.Where(predicate);
        }

        public virtual IQueryable<TEntity> Filter(
           Expression<Func<TEntity, bool>> filter,
           Func<IQueryable<TEntity>,
               IOrderedQueryable<TEntity>> orderBy,
           out int total,
           int index = 0,
           int size = 25)
        {
            int skipCount = index * size;
            IQueryable<TEntity> resetSet =
                orderBy(filter != null ? AllAsQueryable.Where(filter) : AllAsQueryable);
            total = resetSet.Count();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            return resetSet;
        }

        public virtual bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.DbSet.Count(predicate) > 0;
        }

        public virtual TEntity Create(TEntity entity, bool autoSave = true)
        {
            return _repository.Create(entity, autoSave);
        }

        public virtual TEntity Update(TEntity entity, bool autoSave = true)
        {
            return _repository.Update(entity, autoSave);
        }

        public virtual void Delete(object id,
                                  bool directly = false,
                                  bool autoSave = true,
                                  Type[] notIncludedEntityTypes = null)
        {
            TEntity entityToDelete = ByID(id);
            Delete(entityToDelete, autoSave);
        }

        public virtual void Delete(TEntity entityToDelete, bool autoSave)
        {
            _repository.Delete(entityToDelete, autoSave);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate,
                                   bool directly = false,
                                   bool autoSave = true,
                                   Type[] notIncludedEntityTypes = null)
        {
            IQueryable<TEntity> entitiesToDelete = Filter(predicate);

            foreach (TEntity entity in entitiesToDelete)
                Delete(entity, directly: directly, autoSave: false, notIncludedEntityTypes: notIncludedEntityTypes);

            if (autoSave)
                Save();
        }

        public virtual void Delete(IEnumerable<TEntity> entitiesToDelete,
                                   bool directly = false,
                                   bool autoSave = true,
                                   Type[] notIncludedEntityTypes = null)
        {
            foreach (TEntity entity in entitiesToDelete)
                Delete(entity, directly: directly, autoSave: false, notIncludedEntityTypes: notIncludedEntityTypes);

            if (autoSave)
                Save();
        }

        public virtual void ChangeEntityState(TEntity entity, EntityState state)
        {
            _repository.DbContext.Entry(entity).State = state;
        }

        public virtual void Save()
        {
            _repository.DbContext.SaveChanges();
        }
    }
}
