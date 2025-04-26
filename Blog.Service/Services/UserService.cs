using AutoMapper;
using Blog.Common.Dtos;
using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.Data.Contracts;
using Blog.Data.Entities;
using Blog.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using StatusGeneric;

namespace Blog.Service.Services;

public class UserService(IMapper mapper,
    IBaseRepository<User> baseRepository,
    IOtpService otpService,
    IRedisService redisService,
    IBaseRepository<Gender> baseRepository2) : StatusGenericHandler, IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly IBaseRepository<User> _userRepository = baseRepository;
    private readonly IOtpService _otpService = otpService;
    private readonly IRedisService _redisService = redisService;
    private readonly IBaseRepository<Gender> _genderRepository = baseRepository2;
    private const string userOfkey = "user";
    public Task<List<UserDto>> GetAllUsers()
    {

        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserById(Guid id)
    {
        throw new NotImplementedException();
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

    public Task Login(LoginModel model)
    {
        throw new NotImplementedException();
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

    public async Task VerifyRegister(OtpModel model)
    {
        await _otpService.VerifyAsync(model);
        var user = await _redisService.GetAsync<User>(userOfkey);
        if (user is null)
        {
            AddError("Verification is failed, please register");
            return;
        }
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    public Task VerifyLogin(OtpModel model)
    {
        throw new NotImplementedException();
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
}


