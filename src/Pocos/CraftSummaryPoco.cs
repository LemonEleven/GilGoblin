namespace GilGoblin.Pocos;

public class CraftSummaryPoco
{
    public int WorldID { get; set; }
    public int ItemID { get; set; }
    public string Name { get; set; } = "";
    public int VendorPrice { get; set; }
    public int IconID { get; set; }
    public int StackSize { get; set; }
    public float AverageListingPrice { get; set; }
    public float AverageSold { get; set; }
    public float CraftingCost { get; set; }
    public float CraftingProfitVsSold { get; set; }
    public float CraftingProfitVsListings { get; set; }
    public IEnumerable<IngredientPoco> Ingredients { get; set; } = new List<IngredientPoco>();

    public CraftSummaryPoco() { }

    public CraftSummaryPoco(
        int worldID,
        int itemID,
        string name,
        int vendorPrice,
        int iconID,
        int stackSize,
        float averageListingPrice,
        float averageSold,
        float craftingCost,
        float craftingProfitVsSold,
        float craftingProfitVsListings,
        IEnumerable<IngredientPoco> ingredients
    )
    {
        WorldID = worldID;
        ItemID = itemID;
        Name = name;
        VendorPrice = vendorPrice;
        IconID = iconID;
        StackSize = stackSize;
        AverageListingPrice = averageListingPrice;
        AverageSold = averageSold;
        CraftingCost = craftingCost;
        CraftingProfitVsSold = craftingProfitVsSold;
        CraftingProfitVsListings = craftingProfitVsListings;
        Ingredients = ingredients;
    }

    public CraftSummaryPoco(
        MarketDataPoco marketData,
        ItemInfoPoco itemInfo,
        int craftingCost,
        IEnumerable<IngredientPoco> ingredients
    )
    {
        WorldID = marketData.WorldID;
        ItemID = marketData.ItemID;
        Name = itemInfo.Name;
        VendorPrice = itemInfo.VendorPrice;
        IconID = itemInfo.IconID;
        StackSize = itemInfo.StackSize;
        AverageListingPrice = marketData.AverageListingPrice;
        AverageSold = marketData.AverageSold;
        CraftingCost = craftingCost;
        CraftingProfitVsListings = marketData.AverageListingPrice - craftingCost;
        CraftingProfitVsSold = marketData.AverageSold - craftingCost;
        Ingredients = ingredients;
    }
}