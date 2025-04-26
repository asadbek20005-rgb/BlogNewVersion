using Blog.Data.DbContexts;
using Blog.Data.Entities;

namespace Blog.Data.Repositories;

public class OtpRepository(BlogDbContext context) : BaseRepostiory<Otp>(context)
{
    public override Task AddAsync(Otp entity)
    {
        entity.IsExpired = true;
        entity.CreatedDate = DateTime.UtcNow;
        return base.AddAsync(entity);   
    }
}
    