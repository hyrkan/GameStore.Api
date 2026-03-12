using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var connString = "Data Source=GameStore.db";
var builder = WebApplication.CreateBuilder(args);

//adding validation for every single point in the api to use
builder.Services.AddValidation();
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MigrateDb();
app.MapGamesEndpoints();
app.MapGet("/", () => "Hello World!");
app.Run();


