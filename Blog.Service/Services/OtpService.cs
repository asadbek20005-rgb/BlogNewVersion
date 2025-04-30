using AutoMapper;
using Blog.Common.Models.Otp;
using Blog.Data.Contracts;
using Blog.Data.Entities;
using Blog.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Service.Services;

public class OtpService(IBaseRepository<Otp> baseRepository,
    IRedisService redisService,
    IMapper mapper) : IOtpService
{
    private readonly IBaseRepository<Otp> _otpRepository = baseRepository;
    private readonly IRedisService _redisService = redisService;
    public async Task VerifyAsync(OtpModel model)
    {
        int? code = await _redisService.GetAsync<int>(model.PhoneNumber);
        if (!code.HasValue || code != model.Code)
        {
            throw new ArgumentException("Invalid phone number or code");

        }

        if (await CodeIsExpired(model.Code))
        {
            throw new ArgumentException("Code is expired");

        }
        else
        {


        var otp = new Otp
        {
            PhoneNumber = model.PhoneNumber,
            Code = model.Code,
            CreatedDate = DateTime.UtcNow,
            IsExpired = true
        };
        await _otpRepository.AddAsync(otp);
        await _otpRepository.SaveChangesAsync();
        }
    }

    public int GenerateCode()
    {
        return new Random().Next(1111, 9999);
    }


    private async Task<bool> CodeIsExpired(int code)
    {
        var otp = await _otpRepository.GetAll()
           .Where(o => o.Code == code)
           .AnyAsync(o => o.IsExpired == true);
        return otp;
    }
}
