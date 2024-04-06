using System.ComponentModel.DataAnnotations;

namespace market_tracker_webapi.Application.Http.Models.Product;

public class ProductUpdateInputModel
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name too long.")]
    public string Name { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    [Required]
    public int? Quantity { get; set; } = 1;

    [Required]
    [RegularExpression(
        "^(unidades|kilogramas|gramas|litros|mililitros)$",
        ErrorMessage = "Wrong unit provided. Must be 'unidades', 'kilogramas', 'gramas', 'litros' or 'mililitros'."
    )]
    public string Unit { get; set; } = "unidades";

    [Required]
    public string BrandName { get; set; }

    [Required]
    public int? CategoryId { get; set; }
}