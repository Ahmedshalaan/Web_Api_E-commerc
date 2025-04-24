using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    //When I use This Rrecord? 
    // I depend base on Value Type not Reference Type
    // objects are immutable
    public record ProductResultDto
    {
        public int Id { get; set; }  // Id help Frontend
        public   string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }

    }
}
