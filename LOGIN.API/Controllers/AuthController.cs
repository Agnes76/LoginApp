using LOGIN.Core;
using LOGIN.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LOGIN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _auth;
        public AuthController(IAuthentication auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto userRequest)
        {
            try
            {
                return Ok(await _auth.Login(userRequest));
            }
            catch (AccessViolationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registration)
        {
            try
            {
                var result = await _auth.Register(registration);
                return Created("", result);
                // return CreatedAtAction(nameOf(GetProduct),new {Id = result.Id}, result);
            }
            catch (MissingFieldException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
