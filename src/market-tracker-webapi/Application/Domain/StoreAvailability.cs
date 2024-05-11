namespace market_tracker_webapi.Application.Domain;

public record StoreAvailability(
    int StoreId,
    string ProductId,
    int CompanyId,
    bool IsAvailable,
    DateTime LastChecked
);