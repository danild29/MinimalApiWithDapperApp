

namespace MinimalApiWithDapper;
public static class Api
{

    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Users", GetUsers);
        app.MapGet("/Users/{Id}", GetUser);
        app.MapPost("/Users", InsertUser);
        app.MapPut("/Users", UpdateUser);
        app.MapDelete("/Users", DeleteUser);
    }

    private static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetUser(int Id, IUserData data)
    {
        try
        {
            var res = await data.GetUser(Id);
            if (res == null) return Results.NotFound();
            return Results.Ok(res);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> DeleteUser(int Id, IUserData data)
    {
        try
        {
            await data.DeleteUser(Id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
