namespace _4_IWantApp.Endpoints.Categories
{
    public record ProductResponse(
        string Name,
        string CategoryName,
        string Description,
        bool HasStock,
        bool Active,
        decimal Price
        );
}