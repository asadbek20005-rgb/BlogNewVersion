using Blog.Common.Dtos;
using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
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
    Task<UserDto> GetUserById(Guid id);

}
