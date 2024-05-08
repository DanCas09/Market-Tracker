using market_tracker_webapi.Application.Domain;
using market_tracker_webapi.Application.Domain.Filters.Product;
using market_tracker_webapi.Application.Domain.Models.Market.Retail.Sales;
using market_tracker_webapi.Application.Domain.Models.Market.Store;

namespace market_tracker_webapi.Application.Repository.Market.Price;

public interface IPriceRepository
{
    /**
     *  Get the best offers products available in the market applying filters at current time.
     */
    public Task<PaginatedProductOffers> GetBestAvailableProductsOffersAsync(
        int skip,
        int take,
        ProductsSortOption? sortBy = null,
        string? name = null,
        IList<int>? categoryIds = null,
        IList<int>? brandIds = null,
        int? minPrice = null,
        int? maxPrice = null,
        int? minRating = null,
        int? maxRating = null,
        IList<int>? companyIds = null,
        IList<int>? storeIds = null,
        IList<int>? cityIds = null
    );

    /**
     * Get the cheapest store price available for a product at current time.
     */
    public Task<StorePrice?> GetCheapestStorePriceAvailableByProductIdAsync(
        string productId,
        IList<int>? companyIds = null,
        IList<int>? storeIds = null,
        IList<int>? cityIds = null
    );

    public Task<IEnumerable<StoreAvailability>> GetStoresAvailabilityAsync(string productId);

    public Task<StoreAvailability?> GetStoreAvailabilityStatusAsync(string productId, int storeId);

    public Task<StorePrice?> GetStorePriceAsync(
        string productId,
        int storeId,
        DateTime priceAt
    );

    public Task<IEnumerable<Domain.Models.Market.Retail.Sales.Pricing.Price>> GetPriceHistoryByProductIdAndStoreIdAsync(
        string productId,
        int storeId,
        DateTime pricedAfter,
        DateTime pricedBefore
    );

    public Task<string> AddPriceAsync(
        string productId,
        int storeId,
        int price,
        int? promotionPercentage
    );
}