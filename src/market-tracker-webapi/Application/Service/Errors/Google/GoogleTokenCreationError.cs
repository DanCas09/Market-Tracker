﻿namespace market_tracker_webapi.Application.Service.Errors.Google;

public class GoogleTokenCreationError: IServiceError
{
    public class InvalidIssuer(string issuerName) : GoogleTokenCreationError
    {
    }

    public class InvalidValue() : GoogleTokenCreationError
    {
    }
}