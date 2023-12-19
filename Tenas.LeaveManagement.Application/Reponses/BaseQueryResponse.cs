
namespace Tenas.LeaveManagement.Application.Reponses
{
    public class BaseQueryResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Operation successful";
        public object? Data { get; set; }
    }
}
