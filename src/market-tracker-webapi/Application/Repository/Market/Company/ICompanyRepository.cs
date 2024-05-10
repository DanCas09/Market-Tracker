﻿using market_tracker_webapi.Application.Domain.Models.Market.Retail.Shop;

namespace market_tracker_webapi.Application.Repository.Market.Company;

using Company = Domain.Models.Market.Retail.Shop.Company;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompaniesAsync();

    Task<Company?> GetCompanyByIdAsync(int id);

    Task<Company?> GetCompanyByNameAsync(string name);

    Task<CompanyId> AddCompanyAsync(string name);

    Task<Company?> UpdateCompanyAsync(int id, string name);

    Task<Company?> DeleteCompanyAsync(int id);
}