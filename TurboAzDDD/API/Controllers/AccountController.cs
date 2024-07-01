using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Utilities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPut]
        [Route("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId is null || token is null) return BadRequest("The values for Id or Token are either not valid or missing");

            if (await _userService.ConfirmEmailAsync(userId, token)) return Ok("The Email confirmed");
            else return BadRequest("The Email couldn't be confirmed");

        }

        [HttpPost]
        [Route("forgetPassword")]
        public async Task ForgetPasswordAsync(ForgetDto forgetDto)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            await _userService.ForgetPasswordAsync(forgetDto, baseUrl);
        }


        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetDto resetDto)
        {
            if (await _userService.ResetPasswordAsync(resetDto)) return Ok("Password is reseted");
            else return BadRequest("Password couldn't be reseted");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            return StatusCode(200, await _userService.Login(loginDto));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterDto registerDto)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            if (await _userService.RegisterAsync(registerDto, baseUrl)) return Ok("The user is created. Please check your email for email verification!");
            else return BadRequest("The user coulnd't be created");

        }

        [Authorize]
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            return StatusCode(200, _userService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOneUserById/{id}")]
        public async Task<IActionResult> GetOneUserById(string id)
        {
            if (id is null) return BadRequest("The Id value is either not valid or missing");
            GetUserDto getUserDto = await _userService.GetOneByIdAsync(id);
            if (getUserDto is not null) return StatusCode(200, getUserDto);
            return BadRequest("The User couldn't be found");
        }

        [HttpGet]
        [Route("getOneByUserNameAsync/{userName}")]
        public async Task<IActionResult> GetOneByUserNameAsync(string userName)
        {
            if (userName is null) return BadRequest("The userName value is either not valid or missing");
            GetUserDto getUserDto = await _userService.GetOneByUserNameAsync(userName);
            if (getUserDto is not null) return StatusCode(200, getUserDto);
            return BadRequest("The User couldn't be found");
        }

        [HttpGet]
        [Route("getOneByEmailAsync/{email}")]
        public async Task<IActionResult> GetOneByEmailAsync(string email)
        {
            if (email is null) return BadRequest("The email value is either not valid or missing");
            GetUserDto getUserDto = await _userService.GetOneByEmailAsync(email);
            if (getUserDto is not null) return StatusCode(200, getUserDto);
            return BadRequest("The User couldn't be found");
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id is null) return BadRequest("The Id value is either not valid or missing");
            if (await _userService.DeleteAsync(id)) return Ok();
            else return BadRequest("The user couldn't be deleted");
        }

       
    }
}
        




