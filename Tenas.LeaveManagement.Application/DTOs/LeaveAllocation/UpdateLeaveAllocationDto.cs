﻿using Tenas.LeaveManagement.Application.DTOs.Common;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class UpdateLeaveAllocationDto : BaseDto, ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
