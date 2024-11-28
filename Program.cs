using DashBored.Api.Data;
using DashBored.Api.Endpoints;
using DashBored.Api.Helpers;
using Microsoft.EntityFrameworkCore;

//SQLitePCL.Batteries.Init();
var builder = WebApplication.CreateBuilder(args);

var connectionString = ConnectionHelper.GetConnectionString();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DashBoredContext>(
   options => options.UseNpgsql(connectionString)
);


var app = builder.Build();
app.mapPostEndpoints();
await app.MigrateDbAsync();

app.Run();
 