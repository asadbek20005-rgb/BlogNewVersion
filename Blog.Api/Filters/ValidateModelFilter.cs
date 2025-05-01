using Blog.Service.Contracts;
using Blog.Service.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Api.Filters;

public class ValidateModelFilter(IUserService userService) : ActionFilterAttribute
{
    private readonly IUserService _userService = userService;
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!_userService.IsValid)
        {

            _userService.CopyToModelState(context.ModelState);
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
