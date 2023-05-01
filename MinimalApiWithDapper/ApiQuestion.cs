

using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Xml.Linq;

namespace MinimalApiWithDapper;
public static class ApiQuestion
{

    public static void ConfigureQuestionApi(this WebApplication app)
    {
        app.MapGet("/Questions/{Id}", GetQuestion);
        app.MapPost("/Questions", InsertQuestion);
        app.MapPut("/Questions", UpdateQuestion);
    }



    private static async Task<IResult> GetQuestion(int Id, IQuestionData data)
    {
        try
        {
            var res = await data.GetQuestion(Id);
            if (res == null) return Results.NotFound();
            return Results.Ok(res);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertQuestion(QuestionModel question, IQuestionData data)
    {
        try
        {
            byte[]? t = new byte[251];

            for (int i = 0; i < 250; i++)
                t[i] = (byte)i;


            if (question.ContentType == "Img")
            {
                string path = "logo.png";

                Bitmap image = new Bitmap(Image.FromFile(path));

                if (image == null) return Results.NotFound("the logo is nowhere to be seen!");

                t = ImageToByteArray(image);

            }
            
            //question.Content = t;

            await data.InsertQuestion(question);

            string con = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MinimalApiUserDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "INSERT INTO dbo.[Question]  (Question, Answer, Content, ContentType) VALUES (@Question, @Answer, @Content, @ContentType)";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@Question", "testl");
                oCmd.Parameters.AddWithValue("@Answer", "testl");
                oCmd.Parameters.AddWithValue("@Content", t);
                oCmd.Parameters.AddWithValue("@ContentType", "Img");
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        
                    }
                    myConnection.Close();
                }
                /*
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.Add("@Question", SqlDbType.NVarChar, 50).Value = "testl";
                oCmd.Parameters.Add("@Answer", SqlDbType.NVarChar, 50).Value = "testl";
                oCmd.Parameters.Add("@Content", SqlDbType.VarBinary, 4000).Value = t;
                oCmd.Parameters.Add("@ContentType", SqlDbType.NVarChar, 3).Value = "Img";
                
                oCmd.CommandType = CommandType.Text;
                oCmd.ExecuteNonQuery();
                */
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
           
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return ms.ToArray();
        }
    }
    private static async Task<IResult> UpdateQuestion(QuestionModel question, IQuestionData data)
    {
        try
        {
            await data.UpdateQuestion(question);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
