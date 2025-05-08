namespace Domain.Exceptions.NotFoundExcipitions
{
    public   class BasketNotFoundExcption(string id) : NotFound_Excption($"Basket with id {id} was not found :(")
    {    
    }
}
