using Domain.Entities.CmmonEntities;

namespace Domain.Entities.orderEntities
{
    public class OrderItem: BaseEntitiyID<Guid>
    {
        public ProuductinOrderItem prouductinOrderItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string OrderId { get; set; }
      

    }
}
