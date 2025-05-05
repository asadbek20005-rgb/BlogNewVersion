using AutoMapper;
using Blog.Data.Contracts;
using Blog.Data.Entities;
using Blog.Service.Contracts;

namespace Blog.Service.Dependencies;

public record ServiceDependencies(
    IMapper Mapper,
    IBaseRepository<User> UserRepository,
    IOtpService OtpService,
    IRedisService RedisService,
    IBaseRepository<Gender> GenderRepository,
    IJwtService JwtService,
    Lazy<IContentService> ContentService,
    IMinioService MinioService,
    IBaseRepository<Content> ContentRepository
);