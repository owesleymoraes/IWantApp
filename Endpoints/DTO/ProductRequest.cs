namespace _4_IWantApp.Endpoints.Categories
{
    public record ProductRequest(
        string Name,
        Guid CategoryId,
        string Description,
        bool HasStock,
        bool active);



}