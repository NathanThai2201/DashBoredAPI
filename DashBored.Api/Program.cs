using DashBored.Api.Data;
using DashBored.Api.Endpoints;
using DashBored.Api.Helpers;
using Microsoft.EntityFrameworkCore;

//SQLitePCL.Batteries.Init();
var builder = WebApplication.CreateBuilder(args);

// var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
// builder.WebHost.UseUrls($"http://*:{port}");

// builder.Services.AddHealthChecks();

// var connectionString = ConnectionHelper.GetConnectionString();
// builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DashBoredContext>(
//    options => options.UseNpgsql(connectionString)
// );


var app = builder.Build();
// app.UseHealthChecks("/health");
// app.mapPostEndpoints();
// await app.MigrateDbAsync();

app.Run();
 