using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.DbContexts;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Role> Roles { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Entities.Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public DbSet<Content> Contents { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<PostStatus> PostStatuses { get; set; }

}
