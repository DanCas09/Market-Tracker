﻿using market_tracker_webapi.Application.Domain.Schemas.Market.Inventory.Product;
using market_tracker_webapi.Application.Domain.Schemas.Market.Retail.Shop;

namespace market_tracker_webapi.Application.Domain.Schemas.List;

public record ListEntry(ListEntryId Id, Product Product, StoreItem? Store, int Quantity)
{
    public ListEntry(
        string Id,
        Product Product,
        StoreItem? Store,
        int Quantity
    ) : this(
        new ListEntryId(Id),
        Product,
        Store,
        Quantity
    )
    {
    }
}

public record ListEntryOffer(string Id, ProductOffer ProductOffer, int Quantity);

public record ListEntryId(
    string Value
);