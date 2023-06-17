

using FluentValidation.Results;
using MinimalApiWithDapper.Validators;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

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
        app.MapGet("/UsersByName/{name}", GetUserByName);
    }


    public static string logFile = "LogsInfo/loginfo.txt";

    
    private static async Task<IResult> GetUserByName(string name)
    {
        try
        {
            UserModel user = new();


            string con = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MinimalApiUserDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from dbo.[User] where FirstName=@FirstName";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@FirstName", name);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        user.FirstName = oReader["FirstName"].ToString();
                        user.LastName = oReader["LastName"].ToString();
                        user.Id = (int)oReader["Id"];
                    }
                    myConnection.Close();
                }
            }

            return Results.Ok(user);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
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



    private static string WriteToLog(ValidationResult res)
    {
        List<string> errors = new();
        foreach (ValidationFailure er in res.Errors)
        {
            errors.Add(er.ErrorMessage);

        }

        List<string> file = File.ReadAllLines(logFile).ToList();
        file.Add(DateTime.Now + "\n");
        file.AddRange(errors);
        file.Add(new string('-', 20) + "\n");
        File.WriteAllLines(logFile, file);


        var sg  = new string('1', 10);
        return String.Join(';', errors);
    }

    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            var validator = new UserValidator();
            var result = validator.Validate(user);

            if(result.IsValid == false)
            {
                return Results.Problem(WriteToLog(result));
            }


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
