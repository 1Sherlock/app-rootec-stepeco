using Microsoft.EntityFrameworkCore;

namespace Stepeco.Core.DAL.Repository.Interface
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        DbContext DbContext { get; }
        DbSet<TEntity> DbSet { get; }

        TEntity Create(TEntity entity, bool autoSave = true);

        TEntity Update(TEntity entity, bool autoSave = true);

        void Delete(TEntity entity, bool autoSave = true);
    }
}
