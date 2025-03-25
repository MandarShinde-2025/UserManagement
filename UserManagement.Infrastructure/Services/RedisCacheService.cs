using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace UserManagement.Infrastructure.Services;

public class RedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }


    public async Task<T?> GetAsync<T>(string key)
    {
        var serializedData = await _cache.GetStringAsync(key);
        return serializedData is null ? default : JsonSerializer.Deserialize<T>(serializedData);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry
        };
        var serializedData = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, serializedData, options);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}