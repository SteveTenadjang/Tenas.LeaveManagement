using Microsoft.AspNetCore.Identity;
using Tenas.LeaveManagement.Application.Contracts.Identity;
using Tenas.LeaveManagement.Application.Models.Identity;
using Tenas.LeaveManagement.Identity.Models;

namespace Tenas.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
            => _userManager = userManager;

        public async Task<Employee> GetEmployee(Guid id)
        {
            var employee = await _userManager.FindByIdAsync(id.ToString());
            return new Employee
            {
                Id = id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(s => new Employee
            {
                Id = new Guid(s.Id),
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
            }).ToList();
        }
    }
}
