namespace Tenas.LeaveManagement.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
            :base($"{name} : ({key}) wasn't found") 
        {

        }
    }
}
