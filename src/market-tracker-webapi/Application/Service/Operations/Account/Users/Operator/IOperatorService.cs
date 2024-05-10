﻿using market_tracker_webapi.Application.Domain.Filters;
using market_tracker_webapi.Application.Domain.Models.Account.Users;
using market_tracker_webapi.Application.Http.Models.Identifiers;
using market_tracker_webapi.Application.Http.Models.Schemas.Account.Users.Operator;
using market_tracker_webapi.Application.Service.Errors;
using market_tracker_webapi.Application.Service.Errors.PreRegister;
using market_tracker_webapi.Application.Service.Errors.User;
using market_tracker_webapi.Application.Utils;

namespace market_tracker_webapi.Application.Service.Operations.Account.Users.Operator;

using Operator = Domain.Models.Account.Users.Operator;

public interface IOperatorService
{
    Task<Either<IServiceError, PaginatedResult<OperatorItem>>> GetOperatorsAsync(
        int skip,
        int take
    );

    Task<Either<UserFetchingError, Operator>> GetOperatorByIdAsync(Guid id);

    Task<Either<PreRegistrationFetchingError, UserId>> CreateOperatorAsync(Guid code, string password);

    Task<Either<UserFetchingError, Operator>> UpdateOperatorAsync(
        Guid id,
        int phoneNumber
    );

    Task<Either<UserFetchingError, UserId>> DeleteOperatorAsync(Guid id);
}