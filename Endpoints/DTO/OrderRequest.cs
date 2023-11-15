
namespace _4_IWantApp.Endpoints.DTO
{
    public record OrderRequest(
       List<Guid> ProductIds,
       string DeleveryAdress
    );
}