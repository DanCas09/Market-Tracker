﻿using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Http.Models.Client;
using market_tracker_webapi.Application.Http.Problem;
using market_tracker_webapi.Application.Pipeline.Authorization;
using market_tracker_webapi.Application.Repository.Dto;
using market_tracker_webapi.Application.Repository.Dto.Client;
using market_tracker_webapi.Application.Service.Errors.User;
using market_tracker_webapi.Application.Service.Operations.Client;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Application.Http.Controllers
{
    [ApiController]
    [Route(Uris.Clients.Base)]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        [HttpGet]
        [Authorized([Role.Client])]
        public async Task<ActionResult<PaginatedResult<ClientInfo>>> GetClientsAsync(
            [FromQuery] PaginationInputs paginationInputs,
            [FromQuery] string? username
        )
        {
            return Ok(
                await clientService.GetClientsAsync(username, paginationInputs.Skip, paginationInputs.ItemsPerPage)
            );
        }

        [HttpGet(Uris.Clients.ClientById)]
        public async Task<ActionResult<ClientInfo>> GetClientAsync(Guid id)
        {
            return ResultHandler.Handle(
                await clientService.GetClientAsync(id),
                error =>
                {
                    return error switch
                    {
                        UserFetchingError.UserByIdNotFound idNotFoundError
                            => new UserProblem.UserByIdNotFound(idNotFoundError).ToActionResult()
                    };
                }
            );
        }

        [HttpPost]
        public async Task<ActionResult<GuidOutputModel>> CreateClientAsync(
            [FromBody] ClientCreationInputModel clientInput
        )
        {
            var res = await clientService.CreateClientAsync(
                clientInput.Username,
                clientInput.Name,
                clientInput.Email,
                clientInput.Password,
                clientInput.Avatar
            );

            return ResultHandler.Handle(
                res,
                error =>
                {
                    return error switch
                    {
                        UserCreationError.EmailAlreadyInUse emailAlreadyInUse
                            => new UserProblem.UserAlreadyExists(
                                emailAlreadyInUse
                            ).ToActionResult(),

                        UserCreationError.InvalidEmail invalidEmail
                            => new UserProblem.InvalidEmail(invalidEmail).ToActionResult()
                    };
                },
                idOutputModel =>
                    Created(Uris.Users.BuildUserByIdUri(idOutputModel.Id), idOutputModel)
            );
        }

        [HttpPut(Uris.Clients.ClientById)]
        public async Task<ActionResult<ClientInfo>> UpdateUserAsync(
            Guid id,
            [FromBody] ClientUpdateInputModel clientInput
        )
        {
            return ResultHandler.Handle(
                await clientService.UpdateClientAsync(id, clientInput.Avatar),
                error =>
                {
                    return error switch
                    {
                        UserFetchingError.UserByIdNotFound userByIdNotFound
                            => new UserProblem.UserByIdNotFound(userByIdNotFound).ToActionResult()
                    };
                }
            );
        }
    }
}