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

    public DbSet<Otp> Otps { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>()
            .HasData(
                new Gender
                {
                    Id = 1,
                    Name = "Male",
                    CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Gender
                {
                    Id = 2,
                    Name = "Female",
                    CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });

        modelBuilder.Entity<Role>()
            .HasData(
                new Role
                {
                    Id = 1,
                    Name = "User",
                    CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });
    }

}
