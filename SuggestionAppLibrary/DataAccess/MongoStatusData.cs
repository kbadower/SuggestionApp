using Microsoft.Extensions.Caching.Memory;

namespace SuggestionAppLibrary.DataAccess;

public class MongoStatusData : IStatusData
{
    private readonly IMongoCollection<StatusModel> _statuses;
    private IMemoryCache _cache;
    private const string cacheName = "StatusData";

    public MongoStatusData(IDbConnection db, IMemoryCache cache)
    {
        _statuses = db.StatusCollection;
        _cache = cache;
    }

    public async Task<List<StatusModel>> GetStatusesAsync()
    {
        var output = _cache.Get<List<StatusModel>>(cacheName);

        if (output is null)
        {
            var result = await _statuses.FindAsync(_ => true);
            output = result.ToList();
            _cache.Set(cacheName, output);
        }

        return output;
    }

    public Task CreateStatus(StatusModel status)
    {
        return _statuses.InsertOneAsync(status);
    }
}
