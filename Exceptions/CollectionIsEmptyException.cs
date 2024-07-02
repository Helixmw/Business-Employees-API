using Employees_API.Interfaces;

namespace Employees_API.Exceptions
{
    public class CollectionIsEmptyException : Exception
    {
        public CollectionIsEmptyException()
        {
            
        }

        public CollectionIsEmptyException(string message):base(message)
        {
            
        }
    }
}
