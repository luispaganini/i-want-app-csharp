using IWantApp.Endpoints.Categories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        IdentityUser user = new IdentityUser {
            UserName = employeeRequest.Name,
            Email = employeeRequest.Email
        };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        List<Claim> userClaims = new List<Claim> {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId)
        };

        var claimResult = userManager.AddClaimsAsync(
            user, userClaims
        ).Result;

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/employees/{user.Id}", user.Id);
        }
    }