using Shared.Dto;

namespace Services.Abstractions
{
    public interface IBasketService
    {
        public Task<BasketDTo> GetBasketAsync(string Id);
        public Task<BasketDTo> UpdateBasketAsync(BasketDTo basket);
        public Task<bool> DeleteBasketAsync(string Id);
    }
}
