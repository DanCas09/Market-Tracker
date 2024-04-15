﻿using System.ComponentModel.DataAnnotations;
using market_tracker_webapi.Application.Domain;
using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Http.Models.ListEntry;
using market_tracker_webapi.Application.Http.Problem;
using market_tracker_webapi.Application.Service.Errors.List;
using market_tracker_webapi.Application.Service.Errors.ListEntry;
using market_tracker_webapi.Application.Service.Errors.Product;
using market_tracker_webapi.Application.Service.Errors.Store;
using market_tracker_webapi.Application.Service.Operations.List;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Application.Http.Controllers.List;

public class ListEntryController(
    IListEntryService listEntryService
) : ControllerBase
{
    [HttpPost(Uris.Lists.ListProductsByListId)]
    public async Task<ActionResult<IntIdOutputModel>> AddListEntryAsync(
        int listId,
        [Required] Guid clientId,
        [FromBody] CreationListEntryInputModel inputModel)
    {
        var res = await listEntryService.AddListEntryAsync(listId, clientId, inputModel.ProductId, inputModel.StoreId,
            inputModel.Quantity
        );

        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    ListFetchingError.ListByIdNotFound idNotFoundError
                        => new ListProblem.ListByIdNotFound(idNotFoundError).ToActionResult(),
                    ListUpdateError.ListIsArchived listIsArchivedError
                        => new ListProblem.ListIsArchived(listIsArchivedError).ToActionResult(),
                    ProductFetchingError.UnavailableProductInStore productUnavailableError
                        => new ProductProblem.UnavailableProductInStore(productUnavailableError).ToActionResult(),
                    ProductFetchingError.ProductByIdNotFound productNotFoundError
                        => new ProductProblem.ProductByIdNotFound(productNotFoundError).ToActionResult(),
                    StoreFetchingError.StoreByIdNotFound storeNotFoundError
                        => new StoreProblem.StoreByIdNotFound(storeNotFoundError).ToActionResult(),
                    ListEntryCreationError.ListEntryQuantityInvalid quantityInvalidError
                        => new ListEntryProblem.ListEntryQuantityInvalid(quantityInvalidError).ToActionResult(),
                    ListFetchingError.UserDoesNotOwnList userDoesNotOwnListError
                        => new ListProblem.UserDoesNotOwnList(userDoesNotOwnListError).ToActionResult(),
                    ListEntryCreationError.ProductAlreadyInList productAlreadyInListError
                        => new ListEntryProblem.ProductAlreadyInList(productAlreadyInListError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError().ToActionResult()
                };
            },
            (outputModel) => Created(Uris.Lists.BuildListByIdUri(outputModel.Id), outputModel)
        );
    }

    [HttpPatch(Uris.Lists.ListEntriesByListIdAndProductId)]
    public async Task<ActionResult<ListEntry>> UpdateListEntryAsync(
        int listId,
        [Required] Guid clientId,
        string productId,
        [FromBody] UpdateListEntryInputModel inputModel)
    {
        var res = await listEntryService.UpdateListEntryAsync(listId, clientId, productId, inputModel.StoreId,
            inputModel.Quantity
        );

        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    ListEntryFetchingError.ListEntryByIdNotFound idNotFoundError
                        => new ListEntryProblem.ListEntryByIdNotFound(idNotFoundError).ToActionResult(),
                    ProductFetchingError.UnavailableProductInStore productUnavailableError
                        => new ProductProblem.UnavailableProductInStore(productUnavailableError).ToActionResult(),
                    ListEntryCreationError.ListEntryQuantityInvalid quantityInvalidError
                        => new ListEntryProblem.ListEntryQuantityInvalid(quantityInvalidError).ToActionResult(),
                    ProductFetchingError.ProductByIdNotFound productNotFoundError
                        => new ProductProblem.ProductByIdNotFound(productNotFoundError).ToActionResult(),
                    StoreFetchingError.StoreByIdNotFound storeNotFoundError
                        => new StoreProblem.StoreByIdNotFound(storeNotFoundError).ToActionResult(),
                    ListFetchingError.UserDoesNotOwnList userDoesNotOwnListError
                        => new ListProblem.UserDoesNotOwnList(userDoesNotOwnListError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError().ToActionResult()
                };
            }
        );
    }

    [HttpDelete(Uris.Lists.ListEntriesByListIdAndProductId)]
    public async Task<ActionResult<ListEntry>> DeleteListEntryAsync(
        int listId,
        [Required] Guid clientId,
        string productId)
    {
        var res = await listEntryService.DeleteListEntryAsync(listId, clientId, productId);

        return ResultHandler.Handle(
            res,
            error =>
            {
                return error switch
                {
                    ListEntryFetchingError.ListEntryByIdNotFound idNotFoundError
                        => new ListEntryProblem.ListEntryByIdNotFound(idNotFoundError).ToActionResult(),
                    ListFetchingError.UserDoesNotOwnList userDoesNotOwnListError
                        => new ListProblem.UserDoesNotOwnList(userDoesNotOwnListError).ToActionResult(),
                    _ => new ServerProblem.InternalServerError().ToActionResult()
                };
            },
            _ => NoContent()
        );
    }
}