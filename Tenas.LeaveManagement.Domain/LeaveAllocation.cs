using Tenas.LeaveManagement.Domain.Common;

namespace Tenas.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
        public int NumberOfDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int Period { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
