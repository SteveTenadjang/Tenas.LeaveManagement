﻿using Microsoft.AspNetCore.Mvc;
using Tenas.LeaveManagement.Application.Models.Identity;
using Tenas.LeaveManagement.Application.Contracts.Identity;

namespace Tenas.LeaveManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
            => Ok(await _authService.Login(request));

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
            => Ok(await _authService.Register(request));
    }
}
