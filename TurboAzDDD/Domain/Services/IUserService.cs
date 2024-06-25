using System;
using System.Net.Mail;
using Domain.DTOs.Tag;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.ENUMs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto, string baseUrl);
        string GenerateJwtToken(AppUser user);
        Task<bool> ConfirmEmailAsync(string userId, string token);
        void Send(string to, string subject, string body);
        Task<string> Login(LoginDto loginDto);
        Task ForgetPasswordAsync(ForgetDto forgetDto, string baseUrl);
        Task<bool> ResetPasswordAsync(string userId, string token, string newPassword);
        List<GetUserDto> GetAllAsync();
        Task<GetUserDto> GetOneByIdAsync(string userId);
        Task<GetUserDto> GetOneByUserNameAsync(string userName);
        Task<GetUserDto> GetOneByEmailAsync(string email);
        Task<bool> DeleteAsync(string userId);

    }
}

