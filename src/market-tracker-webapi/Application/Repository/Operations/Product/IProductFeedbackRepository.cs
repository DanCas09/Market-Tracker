using market_tracker_webapi.Application.Domain;

namespace market_tracker_webapi.Application.Repository.Operations.Product;

public interface IProductFeedbackRepository
{
    Task<IEnumerable<ProductReview>> GetReviewsByProductIdAsync(string productId);

    Task<ProductReview?> UpsertReviewAsync(
        Guid clientId,
        string productId,
        int rating,
        string? comment
    );

    Task<ProductReview?> RemoveReviewAsync(Guid clientId, string productId);

    Task<PriceAlert> UpsertPriceAlertAsync(Guid clientId, string productId, int priceThreshold);

    Task<PriceAlert?> RemovePriceAlertAsync(Guid clientId, string productId);

    Task<bool> UpdateProductFavouriteAsync(Guid clientId, string productId, bool isFavourite);

    Task<ProductPreferences> GetUserFeedbackByProductId(Guid clientId, string productId);

    public Task<ProductStats?> GetProductStatsByIdAsync(string productId);
}
