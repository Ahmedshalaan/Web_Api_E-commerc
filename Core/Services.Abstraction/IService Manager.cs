namespace Services.Abstractions
{
    //I need Call All Services for IProductService  In One Place(Pramater or Ctro)
    public interface IService_Manager
    {
        //Signature for Each And Every Service
        public IProductService ProductService { get; }
        public IBasketService  basketService { get; }
        public IAuthenticationService   authenticationService { get; }
        public IOrderService    orderService { get; }
        public IpaymentService      ipaymentService { get; }
    }
}
