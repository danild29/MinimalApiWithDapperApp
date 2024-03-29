using DataAccess.DbAccess;
using MinimalApiWithDapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddSingleton<IPairData, PairData>();
builder.Services.AddSingleton<IQuestionData, QuestionData>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


File.WriteAllText(Api.logFile, "");



app.UseHttpsRedirection();

app.ConfigureApi();
app.ConfigurePairApi();
app.ConfigureQuestionApi();


app.Run();
