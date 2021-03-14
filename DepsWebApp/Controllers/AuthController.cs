using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepsWebApp.Authentication;
using DepsWebApp.Filter;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Controller registration
    /// </summary>
    [ApiController]
    [Route("/register")]
    [RegisterExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        /// <inheritdoc/>
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Return token of registration
        /// </summary>
        /// <param name="register"> login and password</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("")]
        [RegisterExceptionFilter]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Register([FromBody] RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                return await _accountService.RegisterAsync(register.Login, register.Password);
            }
            else
            {
                return BadRequest("Invalid Model");
            }
        }
    }
}
