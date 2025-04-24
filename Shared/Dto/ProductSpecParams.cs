namespace Shared.Dto
{
    public class ProductSpecParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public int Pageindex { get; set; } = 1;

        private const int MaxPageSize = 10; // Maximum page size

        private const int DefaultPagesize = 5; // Default page size

        private int pagesize = DefaultPagesize;

        public int Pagesize
        {
            get => pagesize;
            set => pagesize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string? Search { get; set; }


    }
}
