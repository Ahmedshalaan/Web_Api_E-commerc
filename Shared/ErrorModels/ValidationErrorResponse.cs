using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public sealed class ValidationErrorResponse
    {
        public int StatuseCode { get; set; }
        public string ErrorMassage { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
    
}
