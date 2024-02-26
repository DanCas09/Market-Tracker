﻿using market_tracker_webapi.Application.Models;
using market_tracker_webapi.Infrastructure;
using market_tracker_webapi.Infrastructure.PostgresSQLTables;
using Microsoft.EntityFrameworkCore;

namespace market_tracker_webapi.Application.Queries
{
    public class UserQuery(MarketTrackerDataContext marketTrackerDataContext) : IUserQuery
    {   
        public async Task<User?> GetUser(int id)
        {
            return MapUserEntity(await marketTrackerDataContext.User.FindAsync(id));
        }

        private static User? MapUserEntity(UserEntity? userEntity)
        {
            return userEntity is not null ? new User()
            {
                Id = userEntity.Id,
                Name = userEntity.Name
            } : null;
        }
    }
}
