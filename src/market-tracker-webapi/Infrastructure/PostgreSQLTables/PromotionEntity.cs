using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using market_tracker_webapi.Application.Domain;

namespace market_tracker_webapi.Infrastructure.PostgreSQLTables;

[Table("promotion", Schema = "MarketTracker")]
public class PromotionEntity
{
    [Column("percentage")]
    public required int Percentage { get; set; }

    [Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Key]
    [Column("price_entry_id")]
    public required string PriceEntryId { get; set; }

    public Promotion ToPromotion(int oldPrice)
    {
        return new Promotion(Percentage, oldPrice, CreatedAt);
    }
}
