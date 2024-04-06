﻿namespace market_tracker_webapi.Application.Service.Errors.Store;

public class StoreFetchingError : IStoreError
{
    public class StoreByIdNotFound(int id) : StoreFetchingError
    {
        public int Id { get; } = id;
    }
    
    public class StoreByCompanyIdNotFound(int companyId) : StoreFetchingError
    {
        public int CompanyId { get; } = companyId;
    }
    
    public class StoreByCityIdNotFound(int cityId) : StoreFetchingError
    {
        public int CityId { get; } = cityId;
    }
    
    public class StoreByCityNameNotFound(string cityName) : StoreFetchingError
    {
        public string CityName { get; } = cityName;
    }
}