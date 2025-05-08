using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExcipitions
{
   public sealed class DeliveryMethodNotFoundEx(int id) : NotFound_Excption($"No Delivery Method with id {id} not found !!")
    { 
    }
}
