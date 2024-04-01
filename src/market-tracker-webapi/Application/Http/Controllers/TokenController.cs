﻿using market_tracker_webapi.Application.Http.Models.Token;
using market_tracker_webapi.Application.Http.Problem;
using market_tracker_webapi.Application.Service.Errors.Token;
using market_tracker_webapi.Application.Service.Operations.Token;
using Microsoft.AspNetCore.Mvc;

namespace market_tracker_webapi.Application.Http.Controllers
{
    [ApiController]
    [Route(Uris.Tokens.Base)]
    public class TokenController(ITokenService tokenService, ILogger<UserController> logger) : ControllerBase
    {
        [HttpPut]
        public async Task<ActionResult<TokenOutputModel>> CreateTokenAsync(
            [FromBody] TokenCreationInputModel userCredentials
        )
        {
            logger.LogDebug(
                $"Call {nameof(CreateTokenAsync)} with {userCredentials.Email} and {userCredentials.Password}");

            return ResultHandler.Handle(
                await tokenService.CreateTokenAsync(userCredentials.Email, userCredentials.Password),
                error =>
                {
                    return error switch
                    {
                        TokenCreationError.InvalidCredentials invalidCredentials =>
                            new TokenProblem.InvalidCredentials().ToActionResult()
                    };
                },
                tokenOutputModel => new CreatedResult("", tokenOutputModel)
            );
        }


        [HttpDelete]
        public async Task<ActionResult<TokenOutputModel>> DeleteTokenAsync(
            [FromBody] Guid tokenValue
        )
        {
            logger.LogDebug(
                $"Call {nameof(DeleteTokenAsync)} with {tokenValue}");

            return ResultHandler.Handle(
                await tokenService.DeleteTokenAsync(tokenValue),
                error =>
                {
                    return error switch
                    {
                        TokenFetchingError.TokenByTokenValueNotFound tokenByTokenValueNotFound =>
                            new TokenProblem.TokenByTokenValueNotFound(tokenByTokenValueNotFound).ToActionResult()
                    };
                }
            );
        }
    }
}