namespace Domain.Exceptions.NotFoundExcipitions
{
    public sealed class OrderNotFoundEx(Guid id) : NotFound_Excption($"Order with id {id} was not found :(")
    {
    
    }
}
