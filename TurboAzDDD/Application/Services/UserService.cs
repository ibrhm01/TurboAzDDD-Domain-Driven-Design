using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Configuration;
using Application.Exceptions;
using Application.Extensions;
using AutoMapper;
using Domain;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.ENUMs;
using Domain.Exceptions;
using Domain.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;


namespace Application.Services
{
	public class UserService:IUserService
	{
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfig _jwtConfig;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtConfig> jwtConfig, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = jwtConfig.Value;
            _configuration = configuration;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        ///

        public string GenerateJwtToken(AppUser user) 
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", user.Id),
                        //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                    }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;

        }


        public async Task<bool> RegisterAsync(RegisterDto registerDto, string baseUrl)
        {
            if (registerDto is null || baseUrl is null)
                throw new NotValidArgumentValueException("The value for registerDto or baseUrl is not valid");

            var user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user is not null)
                throw new ExistedUserException("There is already such User with this UserName");

            user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null)
                throw new ExistedUserException("There is already such User with this Email. Please Login!");

            if (registerDto.Password != registerDto.RePassword)
                throw new NotValidArgumentValueException("RePassword do not match with the Password you provided!");

            user = new AppUser();

            var mapped = _mapper.Map(registerDto, user);
            
            var CreateUserResult = await _userManager.CreateAsync(mapped, registerDto.Password);


            if (CreateUserResult.Succeeded)
            {
                var AddRoleResult = await _userManager.AddToRoleAsync(user, Role.Member.ToString());
                
                if (!AddRoleResult.Succeeded)
                {
                    string errorMessages = string.Empty;

                    foreach (var error in AddRoleResult.Errors)
                    {
                        errorMessages += error.Description;
                    }
                    throw new NotValidArgumentValueException(errorMessages);
                }

                string path = "wwwroot/Verify.html";
                string body = string.Empty;
                body = await body.ReadFileAsync(path);

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                string link = $"{baseUrl}/api/Account/VerifyEmail?userId={user.Id}&token={token}";


                body = body.Replace("{{link}}", link);
                body = body.Replace("{{Fullname}}", user.FullName);
                string subject = "Verify Email";


                if (user.Email is not null)
                {
                    Send(user.Email, subject, body);

                }
                else throw new NotValidArgumentValueException("The Email is not provided for the user");

                //var token = GenerateJwtToken(user);

                return true;

            }
            else
            {

                string errorMessages = string.Empty;

                foreach (var error in CreateUserResult.Errors) 
                {
                    errorMessages+=error.Description;
                }
                throw new NotValidArgumentValueException(errorMessages);
            }
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            if (userId is null || token is null) throw new NotValidArgumentValueException("Values for userId or token are either missing or not valid");
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) throw new EntityNotFoundException("There is no any User with this userId");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;

        }

        public void Send(string to, string subject, string body)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Smtp:FromAddress").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("Smtp:Server").Value, int.Parse(_configuration.GetSection("Smtp:Port").Value), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Smtp:FromAddress").Value, _configuration.GetSection("Smtp:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            if (user is null) throw new EntityNotFoundException("There is no such User with this UserName");
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (result) return GenerateJwtToken(user);
            else throw new EntityNotFoundException("Password is not correct");
        }

        public async Task ForgetPasswordAsync(ForgetDto forgetDto, string baseUrl)
        {
            if (forgetDto is null || baseUrl is null) throw new NotValidArgumentValueException("The value for registerDto or baseUrl is not valid");

            var user = await _userManager.FindByEmailAsync(forgetDto.Email);
            if (user is null) throw new UserNotFoundException("There is no such user with this Email");

            string path = "wwwroot/ForgetPassword.html";
            string body = string.Empty;
            body = await body.ReadFileAsync(path);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = $"{baseUrl}/api/Account/ResetPasswordAsync?userId={user.Id}&token={token}";
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{Fullname}}", user.FullName);
            string subject = "Forget Password";
            if (user.Email is not null)
            {
                Send(user.Email, subject, body);

            }
            else throw new NotValidArgumentValueException("The Email is not provided for the user");

        }

        public async Task<bool> ResetPasswordAsync(ResetDto resetDto)
        {
            var user = await _userManager.FindByIdAsync(resetDto.UserId);
            if (user is null) throw new UserNotFoundException("There is no such user with this UserId");

            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetDto.Token))
                throw new NotValidArgumentValueException("User is failed to be verified! Provided token does not follow validation rules!");

            if (await _userManager.CheckPasswordAsync(user, resetDto.NewPassword))
                throw new NotValidArgumentValueException("The password provided is the same with the old one!");

            var ResetPasswordResult = await _userManager.ResetPasswordAsync(user, resetDto.Token, resetDto.NewPassword);
            if (ResetPasswordResult.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                return true;
            }
            else
            {
                string errorMessages = string.Empty;

                foreach (var error in ResetPasswordResult.Errors)
                {
                    errorMessages += error.Description;
                }
                throw new NotValidArgumentValueException(errorMessages);
            }
        }

        public List<GetUserDto> GetAllAsync()
        {
            List<GetUserDto> getUserDtos = new();

            List<AppUser> users = _userManager.Users.ToList();


            var mapped = _mapper.Map(users, getUserDtos);
            return mapped;

        }

        public async Task<GetUserDto> GetOneByIdAsync(string userId)
        {
            if (userId is null) throw new NotValidArgumentValueException("The UserId provided is not valid or missing");

            GetUserDto getUserDto = new();

            var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("There is no such User");

            var mapped = _mapper.Map(user, getUserDto);

            return mapped;

        }

        public async Task<GetUserDto> GetOneByUserNameAsync(string userName)
        {
            if (userName is null) throw new NotValidArgumentValueException("The UserName provided is not valid or missing");

            GetUserDto getUserDto = new();

            var user = await _userManager.FindByNameAsync(userName) ?? throw new EntityNotFoundException("There is no such User");

            var mapped = _mapper.Map(user, getUserDto);

            return mapped;

        }

        public async Task<GetUserDto> GetOneByEmailAsync(string email)
        {
            if (email is null) throw new NotValidArgumentValueException("The email provided is not valid or missing");

            GetUserDto getUserDto = new();

            var user = await _userManager.FindByEmailAsync(email) ?? throw new EntityNotFoundException("There is no such User");

            var mapped = _mapper.Map(user, getUserDto);

            return mapped;

        }

        public async Task<bool> DeleteAsync(string userId)
        {
            if (userId is null) throw new NotValidArgumentValueException("The UserId provided is not valid or missing");

            var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("There is no such User");
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;

        }
    }
}

