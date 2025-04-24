using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public sealed class ValidationError
    {
        public string Field { get; set; } //take the Key of the ModelState
        public IEnumerable<string> Errors { get; set; } //Return List of Errors //Take the Value ErrorMessage
    }
}
