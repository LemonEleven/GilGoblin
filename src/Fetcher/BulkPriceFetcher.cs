using GilGoblin.Pocos;
using GilGoblin.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace GilGoblin.Fetcher;

public class PriceBulkFetcher : BulkDataFetcher<PriceWebPoco, PriceWebResponse>, IPriceBulkDataFetcher
{
    private readonly IMarketableItemIdsFetcher _marketableFetcher;

    public PriceBulkFetcher(
        IMarketableItemIdsFetcher marketableFetcher,
        ILogger<PriceBulkFetcher> logger,
        HttpClient? client = null)
        : base(PriceBaseUrl, logger, client)
    {
        _marketableFetcher = marketableFetcher;
    }

    protected override string GetUrlPathFromEntries(IEnumerable<int> ids, int? worldId = null)
    {
        var basePath = base.GetUrlPathFromEntries(ids, worldId);
        return string.Concat(new[] { basePath, SelectiveColumnsMulti });
    }

    public async Task<List<List<int>>> GetIdsAsBatchJobsAsync()
    {
        var allIds = await _marketableFetcher.GetMarketableItemIdsAsync();
        if (!allIds.Any())
            return new List<List<int>>();

        var batcher = new Batcher<int>(PricesPerPage);
        return batcher.SplitIntoBatchJobs(allIds);
    }

    public int PricesPerPage { get; set; } = 100;

    private static string PriceBaseUrl => "https://universalis.app/api/v2/";

    private static string SelectiveColumnsMulti =>
        "?listings=0&entries=0&fields=items.itemID%2Citems.worldID%2Citems.currentAveragePrice%2Citems.currentAveragePriceNQ%2Citems.currentAveragePriceHQ,items.averagePrice%2Citems.averagePriceNQ%2Citems.averagePriceHQ%2Citems.lastUploadTime";
}