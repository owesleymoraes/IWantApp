using _4_IWantApp.Endpoints.DTO;
using _4_IWantApp.Infra.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _4_IWantApp.Endpoints.Employees
{
    public class GetAllEmployee
    {
        public static string Template => "/employees";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(int? page, int? rows, QueryAllUserWithClaimName query)
        {
            return Results.Ok(query.Execute(page.Value, rows.Value));
        }
    }
}