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
    public class BrandResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
