namespace Domain.Entities.orderEntities
{
    public class ProuductinOrderItem
    {
        public ProuductinOrderItem()
        {
        }

        public ProuductinOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
