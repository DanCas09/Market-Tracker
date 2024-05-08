﻿using market_tracker_webapi.Application.Domain;
using market_tracker_webapi.Application.Domain.Models.Market.Store;
using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Http.Models.City;
using market_tracker_webapi.Application.Http.Models.Identifiers;
using market_tracker_webapi.Application.Http.Pipeline.Authorization;
using market_tracker_webapi.Application.Http.Problem;
using market_tracker_webapi.Application.Service.Errors.City;
using market_tracker_webapi.Application.Service.Operations.Market.City;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Application.Http.Controllers.Market;

[ApiController]
public class CityController(ICityService cityService) : ControllerBase
{
    [HttpGet(Uris.Cities.Base)]
    public async Task<ActionResult<CollectionOutputModel<City>>> GetCitiesAsync()
    {
        var res = await cityService.GetCitiesAsync();
        return ResultHandler.Handle(res, _ => new ServerProblem.InternalServerError().ToActionResult());
    }

    [HttpGet(Uris.Cities.CityById)]
    public async Task<ActionResult<City>> GetCityByIdAsync(int id)
    {
        var res = await cityService.GetCityByIdAsync(id);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CityFetchingError.CityByIdNotFound idNotFoundError
                        => new CityProblem.CityByIdNotFound(idNotFoundError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError().ToActionResult()
                };
            }
        );
    }

    [HttpPost(Uris.Cities.Base)]
    [Authorized([Role.Moderator])]
    public async Task<ActionResult<IntIdOutputModel>> AddCityAsync(
        [FromBody] CityCreationInputModel cityInput
    )
    {
        var res = await cityService.AddCityAsync(cityInput.CityName);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CityCreationError.CityNameAlreadyExists cityNameError
                        => new CityProblem.CityNameAlreadyExists(cityNameError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError().ToActionResult()
                };
            },
            outputModel => Created(Uris.Cities.BuildCategoryByIdUri(outputModel.Id), outputModel)
        );
    }

    [HttpPut(Uris.Cities.CityById)]
    [Authorized([Role.Moderator])]
    public async Task<ActionResult<City>> UpdateCityAsync(
        int id,
        [FromBody] CityUpdateInputModel cityInput
    )
    {
        var res = await cityService.UpdateCityAsync(id, cityInput.CityName);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CityFetchingError.CityByIdNotFound idNotFoundError
                        => new CityProblem.CityByIdNotFound(idNotFoundError).ToActionResult(),
                    CityCreationError.CityNameAlreadyExists cityNameError
                        => new CityProblem.CityNameAlreadyExists(cityNameError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError().ToActionResult()
                };
            }
        );
    }

    [HttpDelete(Uris.Cities.CityById)]
    [Authorized([Role.Moderator])]
    public async Task<ActionResult<IntIdOutputModel>> DeleteCityAsync(int id)
    {
        var res = await cityService.DeleteCityAsync(id);
        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    CityFetchingError.CityByIdNotFound idNotFoundError
                        => new CityProblem.CityByIdNotFound(idNotFoundError).ToActionResult(),
                    _
                        => new ServerProblem.InternalServerError(
                            nameof(CityController)
                        ).ToActionResult()
                };
            }
        );
    }
}