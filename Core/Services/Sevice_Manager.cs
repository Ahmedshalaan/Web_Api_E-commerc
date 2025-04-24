namespace Services
{
    //Primary Constructor
    public class Sevice_Manager : IService_Manager
    {
        private readonly Lazy<IProductService> _productService;

        public Sevice_Manager(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        }

        //Signature for Each And Every Service
        public IProductService ProductService => _productService.Value; // I use Lazy When Return Value
    }

}
