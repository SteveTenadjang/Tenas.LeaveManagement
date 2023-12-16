using Tenas.LeaveManagement.Application.Models.Identity;

namespace Tenas.LeaveManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid id);
    }
}
