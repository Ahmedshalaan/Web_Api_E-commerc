
using Domain;
using Presistence.Data;
using System.Threading.Tasks;

namespace Presistence.Repositraces
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntitiyID<Tkey>
    {
        private readonly ApplicationDbcontext _dbcontext;


        public GenericRepository(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking = false) //Defult value is false
        {
            return
                  AsNoTracking                 //return in natural T
                              ? await _dbcontext.Set<TEntity>().ToListAsync()
                              : await _dbcontext.Set<TEntity>().AsNoTracking().ToListAsync();

            // _dbcontext.Set<TEntity>().Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();

        }


        public async Task<TEntity?> GetByIdAsync(Tkey id) => await _dbcontext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await _dbcontext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity) => _dbcontext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _dbcontext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(absSpecifications<TEntity> specifications) =>
           await ApplySpecification(specifications).ToListAsync();
        public async Task<TEntity?> GetByIdAsync(absSpecifications<TEntity> specifications) =>
            await ApplySpecification(specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(absSpecifications<TEntity> specifications)
        
            => await ApplySpecification(specifications).CountAsync();



        private IQueryable<TEntity> ApplySpecification(absSpecifications<TEntity> specification)
        {
            return Static_SpecificationEvaluator.GetQuery<TEntity>(_dbcontext.Set<TEntity>(), specification);
        }


    }

}
