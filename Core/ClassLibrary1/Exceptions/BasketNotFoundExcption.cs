namespace Domain.Exceptions
{
    public sealed class BasketNotFoundExcption(string id) : NotFound_Excption($"Basket with id {id} was not found :(")
    {    
    }
}
