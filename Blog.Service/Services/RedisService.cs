using Blog.Service.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Blog.Service.Services;

public class RedisService(IConnectionMultiplexer connectionMultiplexer) : IRedisService
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();


    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty)
        { 
            return default;
        }

        var jsonValue = JsonConvert.DeserializeObject<T>(value!);
        return jsonValue;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expire = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var jsonValue = JsonConvert.SerializeObject(value);
        await _database.StringSetAsync(key, jsonValue);
    }
}
