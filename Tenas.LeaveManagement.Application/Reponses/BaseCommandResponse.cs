namespace Tenas.LeaveManagement.Application.Reponses
{
    public class BaseCommandResponse<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Operation Successful";
        public T Data { get; set; }
    }
}
