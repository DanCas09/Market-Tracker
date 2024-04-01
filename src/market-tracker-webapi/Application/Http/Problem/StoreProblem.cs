﻿using market_tracker_webapi.Application.Service.Errors.Store;

namespace market_tracker_webapi.Application.Http.Problem;

public class StoreProblem(
    int status,
    string subType,
    string title,
    string detail,
    object? data = null
) : Problem(status, subType, title, detail, data)
{
    public class StoreByIdNotFound(StoreFetchingError.StoreByIdNotFound data)
        : CompanyProblem(
            404,
            "store-not-found",
            "Store not found",
            $"Store with id {data.Id} not found",
            data
        );
    
    public class StoreNameAlreadyExists(StoreCreationError.StoreNameAlreadyExists data)
        : CompanyProblem(
            400,
            "store-name-already-exists",
            "Store name already exists",
            $"Store with name {data.StoreName} already exists",
            data
        );
    
    public class StoreByCompanyIdNotFound(StoreFetchingError.StoreByCompanyIdNotFound data)
        : CompanyProblem(
            404,
            "store-not-found",
            "Store not found",
            $"Store with company id {data.CompanyId} not found",
            data
        );
    
    public class StoreByCityNameNotFound(StoreFetchingError.StoreByCityNameNotFound data)
        : CompanyProblem(
            404,
            "store-not-found",
            "Store not found",
            $"Store with city name {data.CityName} not found",
            data
        );
    
    public class StoreAddressAlreadyExists(StoreCreationError.StoreAddressAlreadyExists data)
        : CompanyProblem(
            400,
            "store-address-already-exists",
            "Store address already exists",
            $"Store with address {data.Address} already exists",
            data
        );
    
    public class StoreByCityIdNotFound(StoreFetchingError.StoreByCityIdNotFound data)
        : CompanyProblem(
            404,
            "store-not-found",
            "Store not found",
            $"Store with city id {data.CityId} not found",
            data
        );
}