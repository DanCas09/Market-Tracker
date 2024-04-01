﻿using market_tracker_webapi.Application.Domain;
using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Http.Problem;
using market_tracker_webapi.Application.Models;
using market_tracker_webapi.Application.Service.Errors.Company;
using market_tracker_webapi.Application.Service.Operations.Company;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Application.Http.Controllers;

[ApiController]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    [HttpGet(Uris.Companies.Base)]
    public async Task<ActionResult<IEnumerable<CompanyDomain>>> GetCompaniesAsync()
    {
        var companies = await companyService.GetCompaniesAsync();
        return Ok(companies);
    }
    
    [HttpGet(Uris.Companies.CompanyById)]
    public async Task<ActionResult<CompanyDomain>> GetCompanyByIdAsync(int id)
    {
        var res = await companyService.GetCompanyByIdAsync(id);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyFetchingError.CompanyByIdNotFound idNotFoundError
                        => new CompanyProblem.CompanyByIdNotFound(
                            idNotFoundError
                        ).ToActionResult(),
                    _ => new ServerProblem.InternalServerError(nameof(CompanyController)).ToActionResult()
                };
            }
        );
    }
    
    [HttpPost(Uris.Companies.Base)]
    public async Task<ActionResult<IdOutputModel>> AddCompanyAsync([FromBody] CompanyCreationInputModel companyInput)
    {
        var res = await companyService.AddCompanyAsync(companyInput.CompanyName);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyCreationError.CompanyNameAlreadyExists companyError
                        => new CompanyProblem.CompanyNameAlreadyExists(companyError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError(nameof(CompanyController)).ToActionResult()
                };
            }
        );
    }
    
    [HttpPut(Uris.Companies.CompanyById)]
    public async Task<ActionResult<CompanyDomain>> UpdateCompanyAsync(int id, [FromBody] CompanyUpdateInputModel companyInput)
    {
        var res = await companyService.UpdateCompanyAsync(id, companyInput.CompanyName);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyFetchingError.CompanyByIdNotFound idNotFoundError
                        => new CompanyProblem.CompanyByIdNotFound(
                            idNotFoundError
                        ).ToActionResult(),
                    CompanyCreationError.CompanyNameAlreadyExists companyError
                        => new CompanyProblem.CompanyNameAlreadyExists(companyError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError(nameof(CompanyController)).ToActionResult()
                };
            }
        );
    }
    
    [HttpDelete(Uris.Companies.CompanyById)]
    public async Task<ActionResult<IdOutputModel>> DeleteCompanyAsync(int id)
    {
        var res = await companyService.DeleteCompanyAsync(id);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyFetchingError.CompanyByIdNotFound idNotFoundError
                        => new CompanyProblem.CompanyByIdNotFound(
                            idNotFoundError
                        ).ToActionResult(),
                    _ => new ServerProblem.InternalServerError(nameof(CompanyController)).ToActionResult()
                };
            }
        );
    }
}