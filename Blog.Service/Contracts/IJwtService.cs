using Blog.Data.Entities;

namespace Blog.Service.Contracts;

public interface IJwtService
{
    string CreateToken(User user);

}
