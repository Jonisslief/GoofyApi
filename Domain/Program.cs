using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=LAPTOP-JONISSLI\\MSSQLSERVER01;Database=GoofyDB;Trusted_Connection=True;TrustServerCertificate=True"));
//builder.Services.AddScoped<DataContext>();

var app  = builder.Build();

app.Run();