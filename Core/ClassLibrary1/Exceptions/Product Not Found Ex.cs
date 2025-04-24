namespace Domain.Exceptions
{
    public class Product_Not_Found_Ex : NotFound_Excption
    {
        public Product_Not_Found_Ex(int Id) : base($"Product With Id : {Id }Not Found")
        {

        }
    }
}
