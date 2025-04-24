

using Domain.Entities.CmmonEntities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();
        //Signature for SaveChangesAsync method Will Return an instance of Class Implements IGenericRepository
        
        //new GenericRepository<Product, int>
        //new GenericRepository<ProductType, int>
        //new GenericRepository<ProductBrand, int>
        
        IGenericRepository<TEntity,TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntitiyID<TKey>;
    }
}
