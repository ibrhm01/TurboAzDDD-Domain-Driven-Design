using Domain.DTOs.User;
using Domain.Entities;


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
        Task<bool> ResetPasswordAsync(ResetDto resetDto);
        List<GetUserDto> GetAllAsync();
        Task<GetUserDto> GetOneByIdAsync(string userId);
        Task<GetUserDto> GetOneByUserNameAsync(string userName);
        Task<GetUserDto> GetOneByEmailAsync(string email);
        Task<bool> DeleteAsync(string userId);

    }
}

