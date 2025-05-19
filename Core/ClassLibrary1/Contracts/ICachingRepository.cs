namespace Domain.Contracts
{
    public interface ICachingRepository
    {
        //Set [Key , Value , TimeToLive (exp date)]
       Task SetAsync(string key, object value, TimeSpan duration);
        //Get O references
        Task<string?> GetAsync(string key);
    }
}
