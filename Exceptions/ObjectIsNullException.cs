namespace Employees_API.Exceptions
{
    public class ObjectIsNullException : Exception
    {
        public ObjectIsNullException()
        {
            
        }

        public ObjectIsNullException(string message):base(message)
        {
            
        }
    }
}
