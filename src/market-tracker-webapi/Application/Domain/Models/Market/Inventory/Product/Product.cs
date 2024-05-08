namespace market_tracker_webapi.Application.Domain.Models.Market.Inventory.Product;

public record Product(
    string Id,
    string Name,
    string ImageUrl,
    int Quantity,
    ProductUnit Unit,
    int Views,
    double Rating,
    Brand Brand,
    Category Category
);

public record ProductItem(string ProductId, string Name, string ImageUrl, string BrandName);