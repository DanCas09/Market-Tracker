﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace market_tracker_webapi.Infrastructure.PostgreSQLTables
{
    [Table("store", Schema = "MarketTracker")]
    public class StoreEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Required]
        [StringLength(200)]
        [Column("address")]
        public required string Address { get; set; }

        [Required]
        [StringLength(30)]
        [ForeignKey("city")]
        [Column("city_id")]
        public required int? CityId { get; set; }

        [ForeignKey("company")]
        [Column("company_id")]
        public int CompanyId { get; set; }
    }
}