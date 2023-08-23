using RentalCompany.Models.Base;

namespace RentalCompany.Repository.BaseRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetTableNoTracking();
        IQueryable<TEntity> GetTableAsTracking();

        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
     //   IQueryable<TEntity> FilterBy(Func<TEntity, bool> filter = null);
        Task<bool> ISExist(Guid id);

    }
}
