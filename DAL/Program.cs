using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<DataContext>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
