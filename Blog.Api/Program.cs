using AutoMapper;
using Blog.Api.Filters;
using Blog.Common.Models.Jwt;
using Blog.Common.Models.Minio;
using Blog.Data.Contracts;
using Blog.Data.DbContexts;
using Blog.Data.Entities;
using Blog.Data.Repositories;
using Blog.Service.Contracts;
using Blog.Service.Dependencies;
using Blog.Service.Helpers;
using Blog.Service.Jwt;
using Blog.Service.Minio;
using Blog.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var jwtSection = builder.Configuration.GetSection("JwtSettings").Get<JwtModel>() ?? throw new ArgumentNullException();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ValidateModelFilter>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<OtpVerificationFilter>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepostiory<>));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false,connectTimeout=20000,syncTimeout=20000,defaultDatabase=0"));

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = jwtSection.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSection.Key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };

});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(provider =>
{
    var minioSettings = builder.Configuration.GetSection("MinioSettings").Get<MinioSettings>() ?? throw new ArgumentNullException();
    var minioClient = new MinioService(minioSettings.Endpoint, minioSettings.AccessKey, minioSettings.SecretKey);
    return minioClient;
});

builder.Services.AddScoped(provider =>
    new ServiceDependencies(
       provider.GetRequiredService<IMapper>(),
       provider.GetRequiredService<IBaseRepository<User>>(),
       provider.GetRequiredService<IOtpService>(),
       provider.GetRequiredService<IRedisService>(),
       provider.GetRequiredService<IBaseRepository<Gender>>(),
       provider.GetRequiredService<IJwtService>(),
       provider.GetRequiredService<IContentService>(),
       provider.GetRequiredService<IMinioService>()
       )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAllOrigins");

app.Run();
