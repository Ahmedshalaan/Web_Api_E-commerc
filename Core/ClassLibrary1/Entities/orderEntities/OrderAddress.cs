namespace Domain.Entities.orderEntities
{
    public class OrderAddress
    {
        public OrderAddress() //Update Data Base
         
        {
        }

        public OrderAddress(string fristName, string lastName, string country, string city, string street, User user, string userId)
        {
            FristName = fristName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
            User = user;
            UserId = userId;
        }

        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
