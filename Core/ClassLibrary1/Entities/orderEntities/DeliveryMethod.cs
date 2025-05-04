using Domain.Entities.CmmonEntities;

namespace Domain.Entities.orderEntities
{
    public class DeliveryMethod : BaseEntitiyID<int>
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
