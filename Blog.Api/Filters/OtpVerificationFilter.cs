using Blog.Common.Models.Otp;
using Blog.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Blog.Api.Filters;

public class OtpVerificationFilter(IOtpService otpService) : IAsyncActionFilter
{
    private readonly IOtpService _otpService = otpService;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("model", out var value) || value is not OtpModel model)
        {
            context.Result = new BadRequestObjectResult("Invalid model");
            return;
        }

        try
        {
            await _otpService.VerifyAsync(model);
        }
        catch (ArgumentException ex)
        {
            context.Result = new BadRequestObjectResult(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            context.Result = new BadRequestObjectResult("An error occurred while verifying the OTP");
            return;
        }
        await next();
    }
}
