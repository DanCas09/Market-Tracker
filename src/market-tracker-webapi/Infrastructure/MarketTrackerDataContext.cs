﻿using market_tracker_webapi.Infrastructure.PostgreSQLTables;
using Microsoft.EntityFrameworkCore;

namespace market_tracker_webapi.Infrastructure
{
    public class MarketTrackerDataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserEntity> User { get; set; }

        public DbSet<ProductEntity> Product { get; set; }

        public DbSet<CategoryEntity> Category { get; set; }
    }
}
