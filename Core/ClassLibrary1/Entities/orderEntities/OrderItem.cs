using Domain.Entities.CmmonEntities;

namespace Domain.Entities.orderEntities
{
    public class OrderItem: BaseEntitiyID<Guid>
    {
        public OrderItem()
        {
        }

        public OrderItem(ProuductinOrderItem prouductinOrderItem, int quantity, decimal price  )
        {
            this.prouductinOrderItem = prouductinOrderItem;
            Quantity = quantity;
            Price = price;
           
        }

        public ProuductinOrderItem prouductinOrderItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
