using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Services
{
    //Primary Constructor
    public class Sevice_Manager : IService_Manager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public Sevice_Manager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketReposotory basketService,UserManager<User> userManager,IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketService, _mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, options));
        }

        //Signature for Each And Every Service
        public IProductService ProductService => _productService.Value; // I use Lazy When Return Value

        public IBasketService basketService => _basketService.Value;

        public IAuthenticationService authenticationService => _authenticationService.Value;
    }

}
