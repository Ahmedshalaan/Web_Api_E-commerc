
using Domain.Exceptions.NotFoundExcipitions;

namespace Services
{
    
    public class User_NotFound_Excption(string Email) :  NotFound_Excption($"No User With Eamail {Email} Not Found")
    
    {
       

         
    }
}