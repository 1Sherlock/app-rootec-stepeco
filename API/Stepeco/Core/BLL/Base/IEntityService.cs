using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Stepeco.Core.BLL.Base
{
    public interface IEntityService<TEntity>
        where TEntity : class
    {
        TEntity[] All { get; }

        IQueryable<TEntity> AllAsQueryable { get; }
        int Count { get; }

        TEntity ByID(object id);

        bool Contains(Expression<Func<TEntity, bool>> predicate);

        DbSet<TEntity> AsObjectQuery();

        TEntity Create(TEntity entity, bool autoSave = true);

        TEntity Update(TEntity entity, bool autoSave = true);

        void Delete(Expression<Func<TEntity, bool>> predicate,
                  bool directly = false,
                  bool autoSave = true,
                  Type[] notIncludedEntityTypes = null);

        void Delete(IEnumerable<TEntity> entitiesToDelete,
                    bool directly = false,
                    bool autoSave = true,
                    Type[] notIncludedEntityTypes = null);

        void Delete(object id,
                    bool directly = false,
                    bool autoSave = true,
                    Type[] notIncludedEntityTypes = null);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, out int total,
                                   int index = 0, int size = 25);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        void ChangeEntityState(TEntity entity, EntityState state);

        void Save();
    }
}
