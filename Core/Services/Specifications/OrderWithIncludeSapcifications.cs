using Domain;
using Domain.Entities.orderEntities;

namespace Services.Specifications
{
    internal class OrderWithIncludeSapcifications : absSpecifications<Order>
    {
        public OrderWithIncludeSapcifications(Guid Id) : base(x => x.Id ==  Id)
        {
            
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.OrderItems);
        }
        //return Collection ==>With Email
        public OrderWithIncludeSapcifications(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.OrderItems);
            SetOrderByasce(o => o.OrderDate);
        } 
    
    }
}
