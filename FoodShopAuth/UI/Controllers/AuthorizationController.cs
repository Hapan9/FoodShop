using System;
using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthModel auth)
        {
            try
            {
                var userDto = new UserDto
                {
                    Login = auth.Login,
                    Password = auth.Password
                };

                var response = new
                {
                    access_token = _authorizationService.Login(userDto)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Validate")]
        public IActionResult ValidationToken([FromBody] JwtModel token)
        {
            try
            {
                _authorizationService.CheckToken(token.Token);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}