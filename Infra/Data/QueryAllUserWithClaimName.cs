using _4_IWantApp.Endpoints.DTO;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _4_IWantApp.Infra.Data
{
    public class QueryAllUserWithClaimName
    {
        private readonly IConfiguration configuration;

        public QueryAllUserWithClaimName(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<EmployeeResponse> Execute(int page, int rows)
        {
            var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
            var query =
                @"select Email,ClaimValue as Name
                from AspNetUsers u inner
                join AspNetUserClaims c
                on u.id = c.UserId and claimtype = 'Name'
                order by name
                OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return db.Query<EmployeeResponse>(query, new { page, rows });

        }

    }
}