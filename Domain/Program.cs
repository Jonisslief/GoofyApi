using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var connectionString = "";
var serverVersion = MariaDbServerVersion.AutoDetect(connectionString);

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=LAPTOP-JONISSLI\\MSSQLSERVER01;Database=GoofyDB;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddDbContext<DataContext>(x => x.UseMySql(connectionString, serverVersion));
//builder.Services.AddScoped<DataContext>();

var app  = builder.Build();

app.Run();