using DashBored.Api.Data;
using DashBored.Api.Endpoints;

SQLitePCL.Batteries.Init();
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["DashBored:connectionString"];
builder.Services.AddSqlite<DashBoredContext>(connectionString);


var app = builder.Build();
app.mapPostEndpoints();
app.MigrateDb();

app.Run();
