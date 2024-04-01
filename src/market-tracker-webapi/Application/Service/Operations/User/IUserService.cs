﻿using market_tracker_webapi.Application.Http.Models;
using market_tracker_webapi.Application.Http.Models.User;
using market_tracker_webapi.Application.Service.Errors.User;
using market_tracker_webapi.Application.Utils;

namespace market_tracker_webapi.Application.Service.Operations.User;

public interface IUserService
{
    Task<UsersOutputModel> GetUsersAsync(string? username, Pagination pagination);
    
    Task<Either<UserFetchingError, UserOutputModel>> GetUserAsync(Guid id);

    Task<AuthenticatedUser?> GetUserByToken(Guid tokenValue);
    
    Task<Either<UserCreationError, UserCreationOutputModel>> CreateUserAsync(
        string username,
        string name,
        string email,
        string password,
        int? code = null
    );

    Task<Either<UserFetchingError, UserOutputModel>> UpdateUserAsync(
        Guid id,
        string? name,
        string? username
    );

    Task<Either<UserFetchingError, UserOutputModel>> DeleteUserAsync(Guid id);
}