namespace market_tracker_webapi.Application.Repository.Dto.Product;

public class PaginatedProductOffers(
    PaginatedResult<ProductOffer> paginatedResult,
    ProductsFacetsCounters facetsCounters)
{
    public IEnumerable<ProductOffer> Items { get; } = paginatedResult.Items;
    public ProductsFacetsCounters Facets { get; } = facetsCounters;
    public int CurrentPage { get; } = paginatedResult.CurrentPage;
    public int ItemsPerPage { get; } = paginatedResult.ItemsPerPage;
    public int TotalItems { get; } = paginatedResult.TotalItems;
    public int TotalPages { get; } = paginatedResult.TotalPages;
}

public class ProductsFacetsCounters(
    IEnumerable<FacetCounter> brandFacets,
    IEnumerable<FacetCounter> categoryFacets,
    IEnumerable<FacetCounter> companyFacets)
{
    public IEnumerable<FacetCounter> Brands { get; set; } = brandFacets;
    public IEnumerable<FacetCounter> Categories { get; set; } = categoryFacets;
    public IEnumerable<FacetCounter> Companies { get; set; } = companyFacets;

    public ProductsFacetsCounters(): this(null, null, null) {}
    
    public ProductsFacetsCounters(IEnumerable<FacetCounter> brandFacets, IEnumerable<FacetCounter> categoryFacets) :
        this(
            brandFacets,
            categoryFacets,
            new List<FacetCounter>()
        )
    {
    }
}

public class FacetCounter
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public int Count { get; set; }
}