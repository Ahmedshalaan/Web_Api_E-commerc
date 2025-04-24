using Domain.Entities.CmmonEntities;

namespace Domain.Entities
{
    public class ProductType : BaseEntitiyID<int>
    {
        public string Name { get; set; }

        //Navigational property [Many]
    }
}
