using Blog.Common.Dtos;
using Blog.Common.Models.User;
using Microsoft.AspNetCore.Http;
using StatusGeneric;

namespace Blog.Service.Contracts;

public interface IUserService : IStatusGeneric
{
    Task<int?> Register(RegisterModel model);
    Task VerifyRegister();
    Task<int?> Login(LoginModel model);
    Task<string> VerifyLogin();
    Task Logout();
    Task<bool> UserIsExistDb(string phoneNumber);
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto?> GetProfile(Guid id);
    Task UpdateBio(string bio);
    public Task<string> UploadProfilePicture(Guid userId, IFormFile file);
}
