﻿using EFCore.Models.Dtos.Auth;
using EFCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.Register(registerDto);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return Unauthorized(result);
                }
            }

            return BadRequest(ModelState);
        }

       
        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.LogIn(loginDto);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return Unauthorized(result);
                }
            }
            return BadRequest(ModelState);
        }
        
    }
}
