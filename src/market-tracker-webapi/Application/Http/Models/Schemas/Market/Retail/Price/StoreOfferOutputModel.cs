using market_tracker_webapi.Application.Domain.Schemas.Market.Retail.Sales;
using market_tracker_webapi.Application.Http.Models.Schemas.Market.Retail.City;
using market_tracker_webapi.Application.Service.Results;

namespace market_tracker_webapi.Application.Http.Models.Schemas.Market.Retail.Price;

using Price = Domain.Schemas.Market.Retail.Sales.Pricing.Price;

public record StoreOfferOutputModel(
    int Id,
    string Name,
    string Address,
    bool IsOnline,
    CityOutputModel? City,
    Price Price,
    bool IsAvailable,
    DateTime LastChecked
);

public static class StoreOfferOutputModelMapper
{
    public static StoreOfferOutputModel ToOutputModel(this StoreOffer storeOffer)
    {
        return new StoreOfferOutputModel(
            storeOffer.Store.Id.Value,
            storeOffer.Store.Name,
            storeOffer.Store.Address,
            storeOffer.Store.IsOnline,
            storeOffer.Store.City?.ToOutputModel(),
            storeOffer.PriceData,
            storeOffer.StoreAvailability.IsAvailable,
            storeOffer.StoreAvailability.LastChecked
        );
    }
}