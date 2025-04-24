using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CmmonEntities
{
    public class BaseEntitiyID<Tkey>  
    {
        public   Tkey Id { get; set; } //Guid
    }
}
