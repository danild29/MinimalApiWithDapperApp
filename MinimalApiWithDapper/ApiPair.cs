
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiWithDapper;

public static class ApiPair
{

    public static void ConfigurePairApi(this WebApplication app)
    {
        app.MapGet("/Pairs", GetPairs);
        app.MapGet("/Pairs/{Id}", GetPair);
        app.MapPost("/Pairs", InsertPair);
        app.MapPut("/Pairs", UpdatePair);
        app.MapDelete("/Pairs", DeletePair);
        app.MapGet("/PairsGetAnother/{Id}", GetAnotheIdInPairById);
        app.MapDelete("/DeleteOneOfTheUsers", DeleteOneOfTheUsers);
    }
    private static async Task<IResult> GetPairs(IPairData data)
    {
        try
        {
            return Results.Ok(await data.GetPairs());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetPair(int Id, IPairData data, IUserData userData)
    {
        try
        {
            var res = await data.GetPair(Id);
            if (res == null) return Results.NotFound();

            UserModel? user1 = await userData.GetUser(res.IdFirst);
            UserModel? user2 = await userData.GetUser(res.IdSecond);
            var ans = new { res, user1, user2 };
            return Results.Ok(ans);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertPair(PairModel pair, IPairData data, IUserData userData)
    {
        try
        {
            var pairs = await data.GetPairs();
            if(pairs.Where(p => p.Title == pair.Title).Any()) return Results.Conflict("this title already exists");

            var user1 = await userData.GetUser(pair.IdFirst);
            var user2 = await userData.GetUser(pair.IdSecond);

            if (user1 == null || user2 == null) return Results.BadRequest("one of the users doesnt exist");
            await data.InsertPair(pair);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> UpdatePair(PairModel pair, IPairData data)
    {
        try
        {
            await data.UpdatePair(pair);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> DeletePair(int Id, IPairData data)
    {
        try
        {
            await data.DeletePair(Id);
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetAnotheIdInPairById(int Id, IPairData data)
    {
        try
        {
            var res = await data.GetAnotherIdById(Id);
            if(res == null) return Results.NotFound();

            int? AnotherId = null;

            if (res.IdFirst == Id)
            {
                AnotherId = res.IdSecond;
            }
            else if (res.IdSecond == Id)
            {
                AnotherId = res.IdFirst;
            }
            if (res == null) return Results.NotFound();

             
            return Results.Ok(AnotherId);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteOneOfTheUsers(int Id, int num, IPairData data)
    {
        try
        {
            PairModel? p = await data.GetPair(Id);
            if (p == null) return Results.NotFound();

            if (num == 1)
                p.IdFirst = -1;
            if (num == 2)
                p.IdSecond = -1;

            if(p.IdFirst == -1 && p.IdSecond == -1)
            {
                await data.DeletePair(Id);
            }
            else
            {
                await data.UpdatePair(p);
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
