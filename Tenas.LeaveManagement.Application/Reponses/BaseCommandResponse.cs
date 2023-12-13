namespace Tenas.LeaveManagement.Application.Reponses
{
    public class BaseCommandResponse
    {
        public Guid? Id { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
