using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

var conBuilder = new MySqlConnectionStringBuilder
{
    Server = "127.0.0.1:3306",
    UserID = "u2_Hc6tMrJeZK",
    Password = "tWLf@WyBqo!BUHbOaR8n720u",
    Database = "s2_GoofyApi",
};
var serverVersion = new MariaDbServerVersion(new Version(10,11,14));
builder.Services.AddDbContext<DataContext>(x => x.UseMySql("userid=u2_Hc6tMrJeZK;password=tWLf@WyBqo!BUHbOaR8n720u;host=212.227.171.102;database=s2_GoofyApi;persist security info=True", serverVersion));

//builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=LAPTOP-JONISSLI\\MSSQLSERVER01;Database=GoofyDB;Trusted_Connection=True;TrustServerCertificate=True"));

//builder.Services.AddScoped<DataContext>();

var app  = builder.Build();

app.Run();