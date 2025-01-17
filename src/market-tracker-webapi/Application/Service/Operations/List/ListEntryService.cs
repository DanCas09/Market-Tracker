﻿using market_tracker_webapi.Application.Domain.Filters.List;
using market_tracker_webapi.Application.Domain.Schemas.List;
using market_tracker_webapi.Application.Domain.Schemas.Market.Inventory.Product;
using market_tracker_webapi.Application.Repository.List;
using market_tracker_webapi.Application.Repository.List.ListEntry;
using market_tracker_webapi.Application.Repository.Market.Inventory.Product;
using market_tracker_webapi.Application.Repository.Market.Price;
using market_tracker_webapi.Application.Repository.Market.Store;
using market_tracker_webapi.Application.Service.Errors;
using market_tracker_webapi.Application.Service.Errors.List;
using market_tracker_webapi.Application.Service.Errors.ListEntry;
using market_tracker_webapi.Application.Service.Errors.Product;
using market_tracker_webapi.Application.Service.Errors.Store;
using market_tracker_webapi.Application.Service.Results;
using market_tracker_webapi.Application.Service.Transaction;

namespace market_tracker_webapi.Application.Service.Operations.List;

public class ListEntryService(
    IListRepository listRepository,
    IListEntryRepository listEntryRepository,
    IPriceRepository priceRepository,
    IProductRepository productRepository,
    IStoreRepository storeRepository,
    ITransactionManager transactionManager) : IListEntryService
{
    public async Task<ShoppingListEntriesResult> GetListEntriesAsync(string listId,
        Guid clientId,
        ShoppingListAlternativeType? alternativeType,
        IList<int>? companyIds,
        IList<int>? storeIds,
        IList<int>? cityIds
    )
    {
        return await transactionManager.ExecuteAsync(async () =>
        {
            var list = await listRepository.GetListByIdAsync(listId);
            if (list is null)
                throw new MarketTrackerServiceException(new ListFetchingError.ListByIdNotFound(listId));

            if (!list.BelongsTo(clientId))
                throw new MarketTrackerServiceException(
                    new ListFetchingError.ClientDoesNotBelongToList(clientId, list.Id.Value));

            var listEntries = await listEntryRepository.GetListEntriesAsync(listId);

            ShoppingListEntriesResult? entriesResult;

            switch (alternativeType)
            {
                case ShoppingListAlternativeType.Cheapest:
                    var getEntryDetailsByCriteria = new Func<ListEntry, Task<ListEntryOffer>>(async entry =>
                    {
                        var storeOffer = await priceRepository.GetCheapestStoreOfferAvailableByProductIdAsync(
                            entry.Product.Id.Value, companyIds, storeIds, cityIds);
                        return new ListEntryOffer(
                            entry.Id.Value,
                            new ProductOffer(
                                entry.Product,
                                storeOffer
                            ),
                            entry.Quantity
                        );
                    });
                    entriesResult = await BuildShoppingListEntriesResult(listEntries, getEntryDetailsByCriteria);
                    break;
                default:
                    entriesResult = await BuildShoppingListEntriesResult(listEntries, async entry =>
                    {
                        var storeOffer = entry.Store is not null
                            ? await priceRepository.GetStoreOfferAsync(entry.Product.Id.Value, entry.Store.Id.Value,
                                DateTime.UtcNow)
                            : null;

                        return new ListEntryOffer(
                            entry.Id.Value,
                            new ProductOffer(
                                entry.Product,
                                storeOffer
                            ),
                            entry.Quantity
                        );
                    });
                    break;
            }

            return entriesResult;
        });
    }

    public async Task<ListEntryId> AddListEntryAsync(string listId, Guid clientId,
        string productId, int storeId, int quantity)
    {
        return await transactionManager.ExecuteAsync(async () =>
        {
            if (quantity <= 0)
                throw new MarketTrackerServiceException(
                    new ListEntryCreationError.ListEntryQuantityInvalid(quantity));

            var list = await listRepository.GetListByIdAsync(listId);

            if (list is null)
                throw new MarketTrackerServiceException(new ListFetchingError.ListByIdNotFound(listId));

            if (!list.BelongsTo(clientId))
                throw new MarketTrackerServiceException(
                    new ListFetchingError.ClientDoesNotBelongToList(clientId, listId));

            if (list.ArchivedAt is not null)
                throw new MarketTrackerServiceException(new ListUpdateError.ListIsArchived(listId));

            if (await productRepository.GetProductByIdAsync(productId) is null)
                throw new MarketTrackerServiceException(
                    new ProductFetchingError.ProductByIdNotFound(productId));

            if (await storeRepository.GetStoreByIdAsync(storeId) is null)
                throw new MarketTrackerServiceException(new StoreFetchingError.StoreByIdNotFound(storeId));

            if (await listEntryRepository.GetListEntryByProductIdAsync(listId, productId) is not null)
                throw new MarketTrackerServiceException(
                    new ListEntryCreationError.ProductAlreadyInList(listId, productId));

            var storeAvailability = await priceRepository.GetStoreAvailabilityStatusAsync(productId, storeId);
            if (storeAvailability is null)
                throw new MarketTrackerServiceException(
                    new ProductFetchingError.ProductNotFoundInStore(productId, storeId));

            if (!storeAvailability.IsAvailable)
                throw new MarketTrackerServiceException(
                    new ProductFetchingError.OutOfStockInStore(productId, storeId));

            return await listEntryRepository.AddListEntryAsync(listId, productId, storeId, quantity);
        });
    }

    public async Task<ListEntry> UpdateListEntryAsync(string listId, Guid clientId,
        string entryId, int? storeId, int? quantity = null)
    {
        return await transactionManager.ExecuteAsync(async () =>
        {
            var list = await listRepository.GetListByIdAsync(listId);
            if (list is null)
                throw new MarketTrackerServiceException(new ListFetchingError.ListByIdNotFound(listId));

            if (!list.BelongsTo(clientId))
                throw new MarketTrackerServiceException(
                    new ListFetchingError.ClientDoesNotOwnList(clientId, listId));

            if (list.IsArchived)
                throw new MarketTrackerServiceException(new ListUpdateError.ListIsArchived(listId));

            if (quantity <= 0)
                throw new MarketTrackerServiceException(
                    new ListEntryCreationError.ListEntryQuantityInvalid(quantity));

            var listEntry = await listEntryRepository.GetListEntryByIdAsync(entryId);
            if (listEntry is null)
                throw new MarketTrackerServiceException(
                    new ListEntryFetchingError.ListEntryByIdNotFound(entryId));

            var productId = listEntry.Product.Id.Value;
            if (await productRepository.GetProductByIdAsync(productId) is null)
                throw new MarketTrackerServiceException(
                    new ProductFetchingError.ProductByIdNotFound(productId));

            if (storeId is not null)
            {
                if (await storeRepository.GetStoreByIdAsync(storeId.Value) is null)
                    throw new MarketTrackerServiceException(new StoreFetchingError.StoreByIdNotFound(storeId.Value));

                var storeAvailability = await priceRepository.GetStoreAvailabilityStatusAsync(productId, storeId.Value);
                if (storeAvailability is null)
                    throw new MarketTrackerServiceException(
                        new ProductFetchingError.ProductNotFoundInStore(productId, storeId.Value));
                if (!storeAvailability.IsAvailable)
                    throw new MarketTrackerServiceException(
                        new ProductFetchingError.OutOfStockInStore(productId, storeId.Value));
            }

            var updatedProductInList =
                await listEntryRepository.UpdateListEntryByIdAsync(entryId, storeId, quantity);
            return updatedProductInList!;
        });
    }

    public async Task<ListEntryId> DeleteListEntryAsync(string listId, Guid clientId,
        string entryId)
    {
        return await transactionManager.ExecuteAsync(async () =>
        {
            var list = await listRepository.GetListByIdAsync(listId);
            if (list is null)
                throw new MarketTrackerServiceException(new ListFetchingError.ListByIdNotFound(listId));
            
            if (list.IsArchived)
                throw new MarketTrackerServiceException(new ListUpdateError.ListIsArchived(listId));

            if (!list.BelongsTo(clientId))
                throw new MarketTrackerServiceException(
                    new ListFetchingError.ClientDoesNotOwnList(clientId, listId));

            var listEntry = await listEntryRepository.DeleteListEntryByIdAsync(entryId);
            if (listEntry is null)
                throw new MarketTrackerServiceException(
                    new ListEntryFetchingError.ListEntryByIdNotFound(entryId));

            return listEntry.Id;
        });
    }

    // helper method
    private static async Task<ShoppingListEntriesResult> BuildShoppingListEntriesResult(
        IEnumerable<ListEntry> listEntries, Func<ListEntry, Task<ListEntryOffer>> getEntryDetailsByCriteria
    )
    {
        var listEntriesDetails = new List<ListEntryOffer>();

        var totalPrice = 0;
        var totalProducts = 0;

        foreach (var entry in listEntries)
        {
            var listEntryOffer = await getEntryDetailsByCriteria(entry);

            if (listEntryOffer.ProductOffer.StoreOffer is not null)
            {
                totalPrice += listEntryOffer.ProductOffer.StoreOffer.PriceData.FinalPrice * entry.Quantity;
                totalProducts++;
            }

            listEntriesDetails.Add(listEntryOffer);
        }

        return new ShoppingListEntriesResult
        {
            Entries = listEntriesDetails,
            TotalPrice = totalPrice,
            TotalProducts = totalProducts
        };
    }
}