namespace Services
{
    //Primary Constructor
    public class Sevice_Manager : IService_Manager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;

        public Sevice_Manager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketReposotory basketService)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketService, _mapper));
        }

        //Signature for Each And Every Service
        public IProductService ProductService => _productService.Value; // I use Lazy When Return Value

        public IBasketService basketService => _basketService.Value;
    }

}
