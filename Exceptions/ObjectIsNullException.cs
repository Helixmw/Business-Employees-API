using Employees_API.Interfaces;

namespace Employees_API.Exceptions
{
    public class ObjectIsNullException : Exception, IException<ObjectIsNullException>
    {
        public ObjectIsNullException()
        {
            
        }

        public ObjectIsNullException(string message):base(message)
        {
            
        }
    }
}
