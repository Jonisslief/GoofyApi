using Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstr")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
