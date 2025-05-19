using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Services
{
    //Primary Constructor
    public class Sevice_Manager : IService_Manager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IpaymentService> _paymentService;
        private readonly Lazy<ICacheService> _cacheService;

        public Sevice_Manager(IUnitOfWork unitOfWork, IMapper 
            mapper, IBasketReposotory basketService,UserManager<User> userManager,IOptions<JwtOptions> options,IConfiguration configuration,ICachingRepository cachingRepository)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketService, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, options, mapper));
            _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketService, unitOfWork));
            _paymentService = new Lazy<IpaymentService>(() => new PayementService(basketService,configuration,unitOfWork,mapper));
            _cacheService = new Lazy<ICacheService>(() => new CacheService(cachingRepository));
        }

        //Signature for Each And Every Service
        public IProductService ProductService => _productService.Value; // I use Lazy When Return Value

        public IBasketService basketService => _basketService.Value;

        public IAuthenticationService authenticationService => _authenticationService.Value;

        public IOrderService orderService => _orderService.Value;

        public IpaymentService ipaymentService => _paymentService.Value;
        public ICacheService  cacheService => _cacheService.Value;
    }

}
