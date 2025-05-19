namespace Services
{
    internal class CacheService(ICachingRepository cachingRepository) : ICacheService
    {
      

        public async Task<string?> GetCachedValueAsync(string key)
       => await cachingRepository.GetAsync(key);

    

        public async Task SetCacheValueAsync(string key, string value, TimeSpan expirationTime)
          => await cachingRepository.SetAsync(key, value, expirationTime);
    }
}
