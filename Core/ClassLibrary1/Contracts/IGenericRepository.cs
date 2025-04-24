using Domain.Entities.CmmonEntities;

namespace Domain.Contracts
{
    //<product,int>
    //<productBrands,int>
    //<productType,int>
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntitiyID<Tkey>
    {
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking=false);
        Task<TEntity?> GetByIdAsync(absSpecifications<TEntity> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(absSpecifications<TEntity> specifications);
        Task<int> CountAsync(absSpecifications<TEntity> specifications);
        Task AddAsync(TEntity entity); 
        void Update(TEntity entity);
        void Delete(TEntity entity);
    } 
}
