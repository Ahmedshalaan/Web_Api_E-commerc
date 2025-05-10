using Shared.Dto;

namespace Services.Abstractions
{
    public interface IpaymentService
    {
        //Create or Update  PaymentIntent
        public Task<BasketDTo> CreateOrUpdatePaymentAsync(string BasketId);

         
    }
}
