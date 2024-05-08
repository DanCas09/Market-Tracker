﻿using market_tracker_webapi.Application.Domain.Models.Market.Store;
using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Http.Models.Company;
using market_tracker_webapi.Application.Http.Models.Identifiers;
using market_tracker_webapi.Application.Http.Pipeline.Authorization;
using market_tracker_webapi.Application.Http.Problem;
using market_tracker_webapi.Application.Service.Errors.Company;
using market_tracker_webapi.Application.Service.Operations.Market.Company;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Application.Http.Controllers.Market;

[ApiController]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    [HttpGet(Uris.Companies.Base)]
    public async Task<ActionResult<CollectionOutputModel<Company>>> GetCompaniesAsync()
    {
        var res = await companyService.GetCompaniesAsync();
        return ResultHandler.Handle(
            res,
            _ => new ServerProblem.InternalServerError(
                nameof(CompanyController)
            ).ToActionResult()
        );
    }

    [HttpGet(Uris.Companies.CompanyById)]
    public async Task<ActionResult<Company>> GetCompanyByIdAsync(int id)
    {
        var res = await companyService.GetCompanyByIdAsync(id);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyFetchingError.CompanyByIdNotFound idNotFoundError
                        => new CompanyProblem.CompanyByIdNotFound(idNotFoundError).ToActionResult(),
                    _
                        => new ServerProblem.InternalServerError(
                            nameof(CompanyController)
                        ).ToActionResult()
                };
            }
        );
    }

    [HttpPost(Uris.Companies.Base)]
    [Authorized([Role.Moderator])]
    public async Task<ActionResult<IntIdOutputModel>> AddCompanyAsync(
        [FromBody] CompanyCreationInputModel companyInput
    )
    {
        var res = await companyService.AddCompanyAsync(companyInput.CompanyName);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyCreationError.CompanyNameAlreadyExists companyError
                        => new CompanyProblem.CompanyNameAlreadyExists(
                            companyError
                        ).ToActionResult(),
                    _
                        => new ServerProblem.InternalServerError(
                            nameof(CompanyController)
                        ).ToActionResult()
                };
            },
            outputModel => Created(Uris.Companies.BuildCompanyByIdUri(outputModel.Id), outputModel)
        );
    }

    [HttpPut(Uris.Companies.CompanyById)]
    [Authorized([Role.Moderator])]
    public async Task<ActionResult<Company>> UpdateCompanyAsync(
        int id,
        [FromBody] CompanyUpdateInputModel companyInput
    )
    {
        var res = await companyService.UpdateCompanyAsync(id, companyInput.CompanyName);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyFetchingError.CompanyByIdNotFound idNotFoundError
                        => new CompanyProblem.CompanyByIdNotFound(idNotFoundError).ToActionResult(),
                    CompanyCreationError.CompanyNameAlreadyExists companyError
                        => new CompanyProblem.CompanyNameAlreadyExists(
                            companyError
                        ).ToActionResult(),
                    _
                        => new ServerProblem.InternalServerError(
                            nameof(CompanyController)
                        ).ToActionResult()
                };
            }
        );
    }

    [HttpDelete(Uris.Companies.CompanyById)]
    [Authorized([Role.Moderator])]
    public async Task<ActionResult<IntIdOutputModel>> DeleteCompanyAsync(int id)
    {
        var res = await companyService.DeleteCompanyAsync(id);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CompanyFetchingError.CompanyByIdNotFound idNotFoundError
                        => new CompanyProblem.CompanyByIdNotFound(idNotFoundError).ToActionResult(),
                    _
                        => new ServerProblem.InternalServerError(
                            nameof(CompanyController)
                        ).ToActionResult()
                };
            }
        );
    }
}