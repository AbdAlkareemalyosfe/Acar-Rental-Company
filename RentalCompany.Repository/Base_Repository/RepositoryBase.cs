using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentalCompany.Models.Base;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.BaseRepository
{
    public abstract class RepositoryBase
    {
        #region  Vars / Props

        protected readonly ApplicationDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor 
        public RepositoryBase(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor = null)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public string AcceptLanguageHeader
        {
            get { return GetAcceptedLanguage(); }
        }

        public string GetAcceptedLanguage()
        {
            var acceptLanguageHeader = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                acceptLanguageHeader = _httpContextAccessor.HttpContext.Request
                   .Headers["accept-language"];
            }

            if (acceptLanguageHeader.ToString().StartsWith("ar"))
            {
                return "ar";
            }
            return "en";

        }

    }
    #endregion
    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected RepositoryBase(ApplicationDbContext context) : base(context)
        {
        }


        #region Actions
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);

        }

        public IQueryable<TEntity> GetTableNoTracking()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            return query;
            ////////
        }
        /*   public IQueryable<TEntity> FilterBy(Func<TEntity, bool> filter = null)
           {

               return _context.Set<TEntity>().Where(filter).AsQueryable();
           }
        */
        public IQueryable<TEntity> GetTableAsTracking()
        {
            return _context.Set<TEntity>().AsQueryable();

        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> ISExist(Guid id)
        {
            return _context.Set<TEntity>().AnyAsync(i => i.Id == id);
        }

    }

    #endregion
}

