
namespace _4_IWantApp.Endpoints.DTO
{
    public record EmployeeRequest(
        string Email,
        string Password,
        string Name,
        string EmployeeCode);
}