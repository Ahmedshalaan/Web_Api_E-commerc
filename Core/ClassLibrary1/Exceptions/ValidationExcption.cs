namespace Domain.Exceptions
{
    public class ValidationExcption:Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public ValidationExcption(IEnumerable<string> errors) : base("Validation Faild")
        {
            Errors = errors;
        }
    }
}
