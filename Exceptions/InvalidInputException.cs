namespace Employees_API.Exceptions
{
    public class InvalidInputException : Exception
    {
        public object? Errors { get; set; }
        public InvalidInputException(string message, object errors):base(message)
        {
            this.Errors = errors;
        }

        public InvalidInputException()
        {
            
        }
    }
}
