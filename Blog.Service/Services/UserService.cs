using AutoMapper;
using Blog.Common.Dtos;
using Blog.Common.Models.User;
using Blog.Data.Contracts;
using Blog.Data.Entities;
using Blog.Service.Contracts;
using Blog.Service.Dependencies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using StatusGeneric;

namespace Blog.Service.Services;

public class UserService(ServiceDependencies dependencies) : StatusGenericHandler, IUserService
{
    private readonly IMapper _mapper = dependencies.Mapper;
    private readonly IBaseRepository<User> _userRepository = dependencies.UserRepository;
    private readonly IOtpService _otpService = dependencies.OtpService;
    private readonly IRedisService _redisService = dependencies.RedisService;
    private readonly IBaseRepository<Gender> _genderRepository = dependencies.GenderRepository;
    private readonly IJwtService _jwtService = dependencies.JwtService;
    private readonly Lazy<IContentService> _contentService = dependencies.ContentService;
    private const string userOfkey = "user";
    public async Task<List<UserDto>> GetAllUsers()
    {
        List<User>? users = await _redisService.GetAsync<List<User>>(userOfkey);
        if (users is not null && users.Count == 0)
        {
            return _mapper.Map<List<UserDto>>(users);
        }

        users = await _userRepository.GetAll().ToListAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetProfile(Guid id)
    {
        var user = await _userRepository.GetAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (user is null)
        {
            AddError("User not found");
            return null;
        }
        else
        {
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }

    public async Task<bool> UserIsExistDb(string phoneNumber)
    {
        var user = await _userRepository.GetAll()
            .Where(u => u.PhoneNumber == phoneNumber)
            .FirstOrDefaultAsync();
        if (user is null)
            return false;

        else return true;
    }

    private async Task<bool> UserExistInCache(string phoneNumber)
    {
        User? user = await _redisService.GetAsync<User>(userOfkey);
        if (user is null)
            return false;
        return user.PhoneNumber == phoneNumber;
    }

    public async Task<int?> Login(LoginModel model)
    {
        User? user = await GetUserByPhoneNumberAsync(model.PhoneNumber);
        if (user is null)
        {
            AddError("No such an account");
            return null;
        }

        if (PasswordIsIncorrect(user, model.Password))
        {
            AddError("Password is incorrect");
            return null;
        }


        var code = _otpService.GenerateCode();
        await _redisService.SetAsync(user.PhoneNumber, code);
        await _redisService.SetAsync(userOfkey, user, TimeSpan.FromMinutes(5));
        return code;
    }

    private bool PasswordIsIncorrect(User user, string password)
    {
        var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return true;
        }
        return false;
    }
    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public async Task<int?> Register(RegisterModel model)
    {
        if (await UserIsExistDb(model.PhoneNumber) || await UserExistInCache(model.PhoneNumber))
        {
            AddError("User already exist");
            return null;
        }
        if (await GenderIsNotExistAsync(model.GenderId))
        {
            AddError("Enter valid gender");
            return null;
        }

        var user = _mapper.Map<User>(model);
        user.RoleId = 1;
        user.PasswordHash = HashPassword(user, model.Password);
        await _redisService.SetAsync(userOfkey, user, TimeSpan.FromMinutes(5));
        int code = _otpService.GenerateCode();
        await _redisService.SetAsync(user.PhoneNumber, code);
        return code;
    }

    public async Task VerifyRegister()
    {
        var user = await _redisService.GetAsync<User>(userOfkey);
        if (user is null)
        {
            AddError("Verification is failed, please register");
            return;
        }
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    public async Task<string> VerifyLogin()
    {
        var user = await _redisService.GetAsync<User>(userOfkey);
        if (user is null)
        {
            AddError("Verification is failed, please login");
            return string.Empty;
        }

        string token = _jwtService.CreateToken(user);

        return token;
    }
    private string HashPassword(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>().HashPassword(user, password);
        return passwordHasher;
    }


    private async Task<bool> GenderIsNotExistAsync(int genderId)
    {
        var gender = await _genderRepository.GetAll()
            .Where(g => g.Id == genderId)
            .AnyAsync(x => x.Id == genderId);
        if (gender is false)
            return true;
        else
            return false;
    }

    private async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _userRepository.GetAll()
            .Where(u => u.PhoneNumber == phoneNumber)
            .FirstOrDefaultAsync();
        if (user is null)
        {
            return null;
        }
        return user;
    }

    public Task UpdateBio(string bio)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadProfilePicture(Guid userId, IFormFile file)
    {
        var user = await _userRepository.GetAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();


        if (user is null)
        {
            AddError("User not found");
            return string.Empty;
        }

        var fileName = await _contentService.Value.UploadFileAsync(user.Id, file);
        if (fileName is null)
        {
            AddError("File upload failed");
            return string.Empty;
        }
        user.ImageUrl = fileName;
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return fileName;
    }

    public async Task<Stream> DownloadFileAsync(Guid userId, string fileName)
    {
        var user = await _userRepository
            .GetAll()
            .AnyAsync(x => x.Id == userId);
        if (user is false)
        {
            AddError("User not exist");
            return Stream.Null;
        }
        var fileStream = await _contentService.Value.DownloadFileAsync(userId, fileName);
        return fileStream;
    }

    public async Task UpdateProfileAsync(Guid userId, UpdateProfileModel model)
    {
        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            AddError("No such account");
            return;
        }

        var updatedModel = _mapper.Map(model,user);
        await _userRepository.UpdateAsync(updatedModel);
        await _userRepository.SaveChangesAsync();
    }
}