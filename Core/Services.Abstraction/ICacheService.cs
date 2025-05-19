namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task<string?> GetCachedValueAsync(string key);
        Task SetCacheValueAsync(string key, string value, TimeSpan expirationTime);
    }
}
