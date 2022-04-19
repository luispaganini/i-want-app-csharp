using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        if (!(page == null || rows == null))
        {
            if (rows <= 10)
            {
                page = 1;
                rows = 5;
            }
            else
                return Results.BadRequest();
        }
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}