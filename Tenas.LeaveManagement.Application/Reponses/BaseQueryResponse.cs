
namespace Tenas.LeaveManagement.Application.Reponses
{
    public class BaseQueryResponse
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
