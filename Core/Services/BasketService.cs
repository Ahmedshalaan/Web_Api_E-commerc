using Domain.Exceptions.NotFoundExcipitions;
using Shared.Dto;

namespace Services
{
    public class BasketService(IBasketReposotory basketReposotory,IMapper _mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string Id)

            =>await basketReposotory.DeleteBasketAsync(Id);
        public async Task<BasketDTo> GetBasketAsync(string Id)
        {
           var basket = await basketReposotory.GetBasketAsync(Id);
            return basket is null ? throw new BasketNotFoundExcption(Id) :_mapper.Map<BasketDTo>(basket) ;
            
        }

        public async Task<BasketDTo> UpdateBasketAsync(BasketDTo basket)   
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket); // I Convert this _mapper.Map<CustomerBasket>(basket); becouse I Storage Database
            var updatedBasket = await basketReposotory.CreateOrUpdateBasketAsync(customerBasket);

            // I use Exception Not BasketNotFoundExcption Becouse Not Connection Redis
            return updatedBasket is null ? throw new Exception("Can't Update BasKet :(") : _mapper.Map<BasketDTo>(updatedBasket); 
        }
    }
}
