
using Blog.Data.DbContexts;
using Blog.Data.Entities;

namespace Blog.Data.Repositories;

public class UserRepository(BlogDbContext context) : BaseRepostiory<Entities.User>(context)
{
    public override Task AddAsync(User entity)
    {
        entity.RoleId = 1;
        return base.AddAsync(entity);
    }
}
