using Domain.Entities;

namespace Domain.Contracts
{
    public interface IBasketReposotory
    {
        //Get Basket
        public Task<CustomerBasket?> GetBasketAsync(string Id);

        //Delete Basket
        public Task<bool> DeleteBasketAsync(string Id);
        //CreateOrUpdate Basket
        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan? timetolife=null);

    }
}
