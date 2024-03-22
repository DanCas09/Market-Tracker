using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace market_tracker_webapi.Infrastructure.PostgreSQLTables
{
    [Table("product", Schema = "MarketTracker")]
    public class ProductEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("description")]
        public required string Description { get; set; }

        [Column("image_url")]
        public required string ImageUrl { get; set; }

        [Column("quantity")]
        public required int Quantity { get; set; }

        [DefaultValue("unidades")]
        [Column("unit")]
        public string Unit { get; set; }

        [DefaultValue(0)]
        [Column("views")]
        public int Views { get; set; }

        [DefaultValue(0)]
        [Column("rate")]
        public float Rate { get; set; }

        [Column("brand_id")]
        public required int BrandId { get; set; }

        [Column("category_id")]
        public required int CategoryId { get; set; }
    }
}
