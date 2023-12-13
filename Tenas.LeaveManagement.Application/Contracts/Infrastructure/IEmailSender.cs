using Tenas.LeaveManagement.Application.Models;

namespace Tenas.LeaveManagement.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
