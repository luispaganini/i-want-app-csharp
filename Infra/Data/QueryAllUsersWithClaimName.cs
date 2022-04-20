namespace IWantApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private readonly IConfiguration _configuration;
        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
        {
            var db = new SqlConnection(_configuration["ConnectionStrings:Sqlserver"]);
            string query =
                @"select Email, ClaimValue as Name
                from AspNetUsers u 
                left join AspNetUserClaims c 
                on u.Id = c.UserId and c.ClaimType = 'Name'
                order by name
                OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return await db.QueryAsync<EmployeeResponse>(
                query,
                new { page, rows }
            );
        }

    }
}